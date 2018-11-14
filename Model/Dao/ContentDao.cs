using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;

        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public IPagedList<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IPagedList<Content> ListAllPage(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IPagedList<Content> ListAllByTag(string tag, int page, int pageSize)
        {
            var CT = db.ContentTags.SingleOrDefault(x => x.TagID == tag);
            var model = db.Contents.Where(x => x.ID == CT.ContentID).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return model;      
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        public long Create(Content content)
        {

            //Xử lý alias
            if(string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            content.ViewCount = 0;
            db.Contents.Add(content);
            db.SaveChanges();

            //Xử lý tag
            if(!string.IsNullOrEmpty(content.TaginContent))
            {
                string[] tags = content.TaginContent.Split(',');
                foreach(var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = CheckTag(tagId);

                    //insert to tag table
                    if(!existedTag)
                    {
                        InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    InsertContentTag(content.ID, tagId);
                }
            }

            return content.ID;
        }

        public long Edit(Content content)
        {
            //Xử lý alias
            if(string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            db.SaveChanges();

            //Xử lý tag
            if(!string.IsNullOrEmpty(content.TaginContent))
            {
                RemoveAllContentTag(content.ID);
                string[] tags = content.TaginContent.Split(',');
                foreach(var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = CheckTag(tagId);

                    //insert to tag table
                    if(!existedTag)
                    {
                        InsertTag(tagId, tag);
                    }

                    InsertContentTag(content.ID, tagId);
                }
            }

            return content.ID;
        }

        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public List<Tag> ListTag(long contentId)
        {
            var CT = db.ContentTags.SingleOrDefault(x => x.ContentID == contentId);
            var model = db.Tags.Where(x => x.ID == CT.TagID).ToList();
            return model;
        }
    }
}
