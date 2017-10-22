using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using File = Microsoft.SharePoint.Client.File;
using HttpUtility = System.Web.HttpUtility;
namespace RahyabServices.DataAccess.Core.Sharepoint{
    public class SharepointRepositoryBase<TEntity> where TEntity : class, IEntitySharepointMapper, new(){
        private readonly IDataContextFactory _dataContextFactory;
        public SharepointRepositoryBase(IDataContextFactory databaseFactory){
            _dataContextFactory = databaseFactory;
        }
        public NetworkCredential SharepointCredential { get; set; }
        public string SiteCollection { get; set; }
        public string Url { get; set; }
        public TEntity GetItem(int id){
            throw new NotImplementedException();
        }
        public TEntity GetItemById(int id){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><Query><OrderBy><FieldRef Name='Title' Ascending='TRUE'></FieldRef></OrderBy><Where><Eq><FieldRef Name='ID'  ></FieldRef><Value Type='Number'>" +
                    id + "</Value></Eq></Where></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToEntity<TEntity>();
        }
        public TEntity GetLastItem(){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml =
                    "<View><RowLimit>1</RowLimit><Query><OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy></Query></View>"
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToEntity<TEntity>();
        }
        public TEntity GetItem(string query)
        {
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var q = new CamlQuery
            {
                ViewXml = query

            };
            var items = list.GetItems(q);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToEntity<TEntity>();
        }
        public IEnumerable<TEntity> GetItems(string camlQuery){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var query = new CamlQuery
            {
                ViewXml = camlQuery
            };
            var items = list.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            return items.ProjectToListEntity<TEntity>();
        }
        protected List GetListByName(SharepointDataContext client){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var entity = new TEntity();
            return client.Web.Lists.GetByTitle(entity.GetSharepointListName());
        }
        public int Save(TEntity instance){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var itemCreationInfo = new ListItemCreationInformation();
            var item = list.AddItem(itemCreationInfo);

            //Project your data from entity to ListItem, very simple :)
            item.ProjectListItemFromEntity(instance);
            item.Update();
            client.Load(list);
            client.Load(item);
            client.ExecuteQuery();
            return item.Id;
        }
        public void SetPermission(string[] accountNames, string folderName, string roleDefinitions = "View Only"){
            using (var ctx = _dataContextFactory.GetSharepointDataContext(SiteCollection)){
                var list = GetList(ctx);
                ctx.Load(list, ls => ls.RootFolder.Folders);
                ctx.ExecuteQuery();
                var folder = list.RootFolder.Folders.FirstOrDefault(x => x.Name == folderName);
                var roleDefinition = ctx.Site.RootWeb.RoleDefinitions.GetByName(roleDefinitions);
                var roleBindings = new RoleDefinitionBindingCollection(ctx) {roleDefinition};
                folder.ListItemAllFields.BreakRoleInheritance(false, false); //set folder unique permissions

                // add defualt group ( hrdoc managers)
                Principal hrDocManagersGroup = ctx.Web.SiteGroups.GetById(35);
                folder.ListItemAllFields.RoleAssignments.Add(hrDocManagersGroup, roleBindings);

                // add accounts
                foreach (var acc in accountNames){
                    Principal user = ctx.Web.EnsureUser(@"ab\" + acc);
                    folder.ListItemAllFields.RoleAssignments.Add(user, roleBindings);
                }
                ctx.ExecuteQuery();
            }
        }
        public void ResetPermission(string folderName, string roleDefinitions = "View Only"){
            using (var ctx = _dataContextFactory.GetSharepointDataContext(SiteCollection)){
                var list = GetList(ctx);
                ctx.Load(list, ls => ls.RootFolder.Folders);
                ctx.ExecuteQuery();
                var folder = list.RootFolder.Folders.FirstOrDefault(x => x.Name == folderName);
                folder.ListItemAllFields.ResetRoleInheritance();
                ctx.ExecuteQuery();
            }
        }
        public void RemoveOrResetPermission(string[] accounts, string folderName, string roleDefinitions = "View Only"){
            using (var ctx = _dataContextFactory.GetSharepointDataContext(SiteCollection)){
                var list = GetList(ctx);
                ctx.Load(list, ls => ls.RootFolder.Folders);
                ctx.ExecuteQuery();
                var folder = list.RootFolder.Folders.FirstOrDefault(x => x.Name == folderName);
                ctx.Load(folder.ListItemAllFields.RoleAssignments, icol => icol.Include(i => i.Member));
                ctx.ExecuteQuery();
                var userCounts = Enumerable.Count(folder.ListItemAllFields.RoleAssignments,
                    roleAssignment => roleAssignment.Member.Title != "System Account"
                                      && roleAssignment.Member.PrincipalType == PrincipalType.User);
                if (userCounts <= 1) folder.ListItemAllFields.ResetRoleInheritance();
                else{
                    foreach (var acc in accounts){
                        Principal user = ctx.Web.EnsureUser(@"ab\" + acc);
                        var role = folder.ListItemAllFields.RoleAssignments.GetByPrincipal(user);
                        role?.DeleteObject();
                    }
                }
                ctx.ExecuteQuery();
            }
        }
        protected List GetList(SharepointDataContext client){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var entity = new TEntity();
            return client.Web.Lists.GetById(entity.GetSharepointListId());
        }
        public void AttachFileToListItem(int itemId, string fileName, MemoryStream fileMemoryStream){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var entity = new TEntity();
            entity.GetSharepointListName();
            var attachmentpath = string.Format("Lists/" + entity.GetSharepointListName() + "/Attachments/{0}/{1}",
                itemId, HttpUtility.UrlEncode(fileName));
            var url = _dataContextFactory.ConnectionName + SiteCollection;
            var request = WebRequest.Create(url);
            request.Credentials = _dataContextFactory.Credential;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("x-Vermeer-Content-Type", "application/x-www-form-urlencoded");
            var method = "put document: 12.0.0.4518";
            var serviceName = url + "/_vti_bin/_vti_aut/author.dll";
            var document = attachmentpath;
            var putOption = "overwrite,createdir";
            var postbody = "method={4}&service_name={3}&" +
                           "document=[document_name={0};meta_info=[{1}]]&put_option={2}&comment=&keep_checked_out=false\n";
            method = HttpUtility.UrlEncode(method);
            putOption = HttpUtility.UrlEncode(putOption);
            var metaInfo = string.Empty;
            postbody = string.Format(postbody, document, metaInfo, putOption, serviceName, method);
            var encoding = new ASCIIEncoding();
            var stream = new MemoryStream();
            stream.Write(encoding.GetBytes(postbody), 0, postbody.Length);
            var myBytes = new byte[4096];
            fileMemoryStream.Seek(0, SeekOrigin.Begin);
            while (fileMemoryStream.Read(myBytes, 0, myBytes.Length) > 0) { stream.Write(myBytes, 0, myBytes.Length); }
            try{
                using (var webClient = new WebClient()){
                    webClient.Credentials = _dataContextFactory.Credential;
                    webClient.Headers.Add("Content-Type", "application/x-vermeer-urlencoded");
                    webClient.Headers.Add("X-Vermeer-Content-Type", "application/x-vermeer-urlencoded");
                    var result =
                        Encoding.UTF8.GetString(webClient.UploadData(url + "/_vti_bin/_vti_aut/author.dll", "POST",
                            stream.GetBuffer()));
                }
            }
            catch (Exception ex) {}
            stream.Close();
        }
        public void UploadFile(string fileName, Stream fs){
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var doclib = GetList(client);
            var fi = new FileInfo(fileName);
            client.Load(doclib.RootFolder);
            client.ExecuteQuery();
            var fileUrl = $"{doclib.RootFolder.ServerRelativeUrl}/{fi.Name}";
            File.SaveBinaryDirect(client, fileUrl, fs, true);
        }
        public void Update(TEntity instance){
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var item = list.GetItemById(instance.Id.Value);
            item.ProjectListItemFromEntity(instance);
            item.Update();
            client.ExecuteQuery();
        }
        public void Update(EditRequestSplitLogDto editRequestSplitLogDto, int listItemId){
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var item = list.GetItemById(listItemId);
            item["LegislationDate"] = editRequestSplitLogDto.LegislationDate;
            item["StartDate"] = editRequestSplitLogDto.StartDate;
            item["InterestRate"] = editRequestSplitLogDto.InterestRate;
            item.Update();
            client.ExecuteQuery();
        }
        public void Update(EditRequestClearingLogDto editRequestClearingLogDto, int listItemId){
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url);
            var list = GetList(client);
            var item = list.GetItemById(listItemId);
            item["LegislationDate"] = editRequestClearingLogDto.LegislationDate;
            item.Update();
            client.ExecuteQuery();
        }
        public void Update(EditRequestGivingAChanceLogDto editRequestGivingAChanceLogDto, int listItemId){
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var item = list.GetItemById(listItemId);
            item["LegislationDate"] = editRequestGivingAChanceLogDto.LegislationDate;
            item["StartDate"] = editRequestGivingAChanceLogDto.ExpireDate;
            item["Count"] = editRequestGivingAChanceLogDto.Count;
            item.Update();
            client.ExecuteQuery();
        }
        public void Update(EditRequestImpunityForCrimesLogDto editRequestImpunityForCrimesLogDto, int listItemId){
            var client = _dataContextFactory.GetSharepointDataContext(SiteCollection, Url, SharepointCredential);
            var list = GetList(client);
            var item = list.GetItemById(listItemId);
            item["InterestRate"] = editRequestImpunityForCrimesLogDto.InterestRate;
            item.Update();
            client.ExecuteQuery();
        }
    }
}