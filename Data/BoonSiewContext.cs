using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularApp.Data
{
    public partial class BoonSiewContext : DbContext
    {
        public BoonSiewContext()
        {
        }

        public BoonSiewContext(DbContextOptions<BoonSiewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AttributeGroups> AttributeGroups { get; set; }
        public virtual DbSet<AttributeMaster> AttributeMaster { get; set; }
        public virtual DbSet<AuditMaster> AuditMaster { get; set; }
        public virtual DbSet<BannerMaster> BannerMaster { get; set; }
        public virtual DbSet<CategoryMaster> CategoryMaster { get; set; }
        public virtual DbSet<ClassMaster> ClassMaster { get; set; }
        public virtual DbSet<ContactMaster> ContactMaster { get; set; }
        public virtual DbSet<DealerMaster> DealerMaster { get; set; }
        public virtual DbSet<EmailSubscribers> EmailSubscribers { get; set; }
        public virtual DbSet<EmailTemplateMst> EmailTemplateMst { get; set; }
        public virtual DbSet<EventMaster> EventMaster { get; set; }
        public virtual DbSet<ModelAttributes> ModelAttributes { get; set; }
        public virtual DbSet<ModelImages> ModelImages { get; set; }
        public virtual DbSet<ModelMaster> ModelMaster { get; set; }
        public virtual DbSet<ModuleMaster> ModuleMaster { get; set; }
        public virtual DbSet<PageMaster> PageMaster { get; set; }
        public virtual DbSet<PermissionMaster> PermissionMaster { get; set; }
        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<ServiceBookingMaster> ServiceBookingMaster { get; set; }
        public virtual DbSet<ServiceCentreMaster> ServiceCentreMaster { get; set; }
        public virtual DbSet<ServiceTypeMaster> ServiceTypeMaster { get; set; }
        public virtual DbSet<SettingMaster> SettingMaster { get; set; }
        public virtual DbSet<SpotLightMaster> SpotLightMaster { get; set; }
        public virtual DbSet<TimeSlots> TimeSlots { get; set; }
        public virtual DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public virtual DbSet<VerificationCodes> VerificationCodes { get; set; }
        public virtual DbSet<WarrantyCms> WarrantyCms { get; set; }
        public virtual DbSet<WarrantyMaster> WarrantyMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=192.168.9.114;initial catalog=BoonSiew;persist security info=True;user id=BoonDB;password=B00nOwn#;MultipleActiveResultSets=True;Max Pool Size=5000;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributeGroups>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.Property(e => e.GroupId).HasComment("Unique id assoicated with each attribute group record");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Unique attribute group name");

                entity.Property(e => e.IsActive).HasComment("isactive=1(attribute group is in use) And isactive=0(attribute group is soft deleted & no more in use)");

                entity.Property(e => e.SortOrder).HasComment("Attribute group sort order");
            });

            modelBuilder.Entity<AttributeMaster>(entity =>
            {
                entity.HasKey(e => e.AttributeId);

                entity.Property(e => e.AttributeName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AttributeMaster)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttributeMaster_AttributeGroups");
            });

            modelBuilder.Entity<AuditMaster>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.Property(e => e.AuditId).HasComment("Unique id associated with each audit record");

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("datetime")
                    .HasComment("to identify the time when user changed data");

                entity.Property(e => e.ChangeDesc)
                    .IsUnicode(false)
                    .HasComment("specific format of string to identify the operation done by user. eg. Add Record/Edit Record/Delete Record->ColumnName :oldValue -> newValue,ColumnName2 :oldValue -> newValue,ColumnName3 :oldValue -> newValue,etc");

                entity.Property(e => e.PrimaryKeyValues)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("for identify which row has been affected");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Name of table which is going to be affected.");

                entity.Property(e => e.UserId).HasComment("To identify who do this change");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditMaster)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditMaster_UserMaster");
            });

            modelBuilder.Entity<BannerMaster>(entity =>
            {
                entity.HasKey(e => e.BannerId);

                entity.Property(e => e.BannerId).HasComment("Unique id associated with each banner record");

                entity.Property(e => e.BtnText)
                    .IsRequired()
                    .HasColumnName("btnText")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Text of button displayed on banner");

                entity.Property(e => e.IsActive).HasComment("isactive=1(banner is in use) And isactive=0(banner is soft deleted & no more in use)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Banner image filename");

                entity.Property(e => e.PreBanner)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Preview image file name of banner");

                entity.Property(e => e.PreBtnText)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Button text of preview image ");

                entity.Property(e => e.PreRedirectUrl)
                    .HasColumnName("PreRedirectURL")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Redirection url of button of preview image ");

                entity.Property(e => e.PreShortDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Short description about preview image of banner");

                entity.Property(e => e.PreTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Preview image title of banner");

                entity.Property(e => e.RedirectUrl)
                    .IsRequired()
                    .HasColumnName("RedirectURL")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Url to which user should redirected on by clicking button of banner image");

                entity.Property(e => e.ShortDesc)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Short description about banner");

                entity.Property(e => e.SortOrder).HasComment("Banner sort order");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Unique Banner title");
            });

            modelBuilder.Entity<CategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasComment("Unique id assoicated with each category record");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Unique category name");

                entity.Property(e => e.ClassId).HasComment("Id of class to which current category is belongs to(Referenced to ClassMaster)");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(category is in use) And isactive=0(category is soft deleted & no more in use)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Category image file name");

                entity.Property(e => e.SortOrder).HasComment("Category sort order");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.CategoryMaster)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryMaster_ClassMaster");
            });

            modelBuilder.Entity<ClassMaster>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.Property(e => e.ClassId).HasComment("Unique id assoicated with each class record");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Unique class name");

                entity.Property(e => e.IsActive).HasComment("isactive=1(class is in use) And isactive=0class is soft deleted & no more in use)");

                entity.Property(e => e.SortOrder).HasComment("Class sort order");
            });

            modelBuilder.Entity<ContactMaster>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.ContactId).HasComment("Unique id assoicated with each contact record");

                entity.Property(e => e.Address)
                    .IsUnicode(false)
                    .HasComment("Customer address");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer contact number");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Customer's contacted date");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Static values used for contact department are i)Parts Enquiry, ii)Product Enquiry, iii)Service Enquiry for which customer can contact.");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Customer email address");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(contact is in use) And isactive=0(contact is soft deleted & no more in use)");

                entity.Property(e => e.IsEmail).HasComment("To let accept email notification by customer");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasComment("Customer message");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer vehicle model");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Customer name");

                entity.Property(e => e.RegYear)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer vehicle registration year");

                entity.Property(e => e.VehicleNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer vehicle number");
            });

            modelBuilder.Entity<DealerMaster>(entity =>
            {
                entity.HasKey(e => e.DealerId);

                entity.Property(e => e.DealerId).HasComment("Unique id assoicated with each dealer record");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasComment("Dealer address");

                entity.Property(e => e.CreatedBy).HasComment("UserId who created dealer");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Dealer's created date");

                entity.Property(e => e.DealerName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Unique dealer name");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Dealer emial address");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(dealer is in use) And isactive=0(dealer is soft deleted & no more in use)");

                entity.Property(e => e.Lat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Latitude value of dealer's address");

                entity.Property(e => e.Lng)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Longitude value of dealer's address");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Dealer phone number");

                entity.Property(e => e.SortOrder).HasComment("Dealer sort order");
            });

            modelBuilder.Entity<EmailSubscribers>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Unique id assoicated with each email subscriber record");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Email subscriber's created date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Email address of email subscriber");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("First name of email subscriber");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(email subscriber is active) And isactive=0(email subscriber is deactivated & no more in use)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Last name of email subscriber");
            });

            modelBuilder.Entity<EmailTemplateMst>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.Property(e => e.TemplateId)
                    .HasColumnName("TemplateID")
                    .HasComment("Unique id assoicated with each email template");

                entity.Property(e => e.FromEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("From email address to be displayed in sent mail");

                entity.Property(e => e.FromName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("From name to be displayed in sent mail");

                entity.Property(e => e.MailContent)
                    .HasColumnType("text")
                    .HasComment("Content of email ");

                entity.Property(e => e.MailSubject)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Subject of email ");

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Unique tag value assoicated with each email template");
            });

            modelBuilder.Entity<EventMaster>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventId).HasComment("Unique id assoicated with each event record");

                entity.Property(e => e.CreatedBy).HasComment("UserId who created event");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Event created date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasComment("Brief description abot event");

                entity.Property(e => e.EventType).HasComment("1= Event and 2= news");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Event  file name(image, pdf, doc file)downloaded from frontend if uploaded any");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(event is in use) And isactive=0(event is soft deleted & no more in use)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Event  image file name");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("Unique event title");
            });

            modelBuilder.Entity<ModelAttributes>(entity =>
            {
                entity.HasKey(e => e.PrAttrId)
                    .HasName("PK_ProductAttributes");

                entity.Property(e => e.PrAttrId).HasComment("Unique id assoicated with each model attribute");

                entity.Property(e => e.AttrGroup)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Model attribute group name");

                entity.Property(e => e.AttrValue)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasComment("Model's attribute value");

                entity.Property(e => e.Attribute)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Model's attribute name");

                entity.Property(e => e.GroupId).HasComment("Model attribute group id");

                entity.Property(e => e.ModelId).HasComment("ModelId to which attribute associated");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ModelAttributes)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_ModelAttributes_AttributeGroups");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ModelAttributes)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelAttributes_ModelMaster");
            });

            modelBuilder.Entity<ModelImages>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK_ProductImages");

                entity.Property(e => e.ImageId).HasComment("Unique id assoicated with each model image(model feature) record");

                entity.Property(e => e.ModelId).HasComment("Model id to which model image(model feature) is associated");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Brief fescription about model image(model feature)");

                entity.Property(e => e.SortOrder).HasComment("Sort order of model image(model feature)");

                entity.Property(e => e.SubContent)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Content of model image(model feature)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Title of model image(model feature)");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ModelImages)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImages_ProductMaster");
            });

            modelBuilder.Entity<ModelMaster>(entity =>
            {
                entity.HasKey(e => e.ModelId)
                    .HasName("PK_ProductMaster");

                entity.Property(e => e.ModelId).HasComment("Unique id assoicated with each model");

                entity.Property(e => e.CategoryId).HasComment("Category Id to which current model belongs to");

                entity.Property(e => e.CreatedBy).HasComment("UserId who created model");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Model created date");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(model is in use) And isactive=0(model is soft deleted & no more in use)");

                entity.Property(e => e.ModelImg)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Model image file name");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Category wise Unique model name ");

                entity.Property(e => e.ModelNo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Model number");

                entity.Property(e => e.PublishOn)
                    .HasColumnType("date")
                    .HasComment("Date when model should publish on");

                entity.Property(e => e.Sec1Img)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Image file name of model's 1st section ");

                entity.Property(e => e.Sec1Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Title of model's 1st section ");

                entity.Property(e => e.Sec2Content)
                    .IsUnicode(false)
                    .HasComment("Content of model's 2nd section ");

                entity.Property(e => e.Sec2LeftImg1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Image file name of model's 2nd section's upper left portion ");

                entity.Property(e => e.Sec2LeftImg2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Image file name of model's 2nd section's lower left portion ");

                entity.Property(e => e.Sec2RightImg1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Image file name of model's 2nd section's upper right portion ");

                entity.Property(e => e.Sec2RightImg2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Image file name of model's 2nd section's lower right portion ");

                entity.Property(e => e.Sec2Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Title of model's 2nd section ");

                entity.Property(e => e.Sec3Content)
                    .IsUnicode(false)
                    .HasComment("Content of model's 3rd section ");

                entity.Property(e => e.Sec3Img)
                    .IsUnicode(false)
                    .HasComment("Image file name of model's 3rd section");

                entity.Property(e => e.Sec3VideoUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Video url of model's 3rd section");

                entity.Property(e => e.SortOrder).HasComment("Model sort order");

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Model tag value");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ModelMaster)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductMaster_CategoryMaster");
            });

            modelBuilder.Entity<ModuleMaster>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.Property(e => e.ModuleId).HasComment("Unique id assoicated with each module");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(module is in use) And isactive=0(module is soft deleted & no more in use)");

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("It's fixed like CMS,User Management,Model Management,Booking Service, Dealers, Warranty, Reports,etc");
            });

            modelBuilder.Entity<PageMaster>(entity =>
            {
                entity.HasKey(e => e.PageId);

                entity.Property(e => e.PageId).HasComment("Unique id assoicated with each page");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(page is in use) And isactive=0(page is soft deleted & no more in use)");

                entity.Property(e => e.ModuleId).HasComment("ModuleId to which page is belongs to");

                entity.Property(e => e.PageCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Unique pagecode to identify each page ");

                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Module wise unique page name");

                entity.Property(e => e.TableNames)
                    .IsUnicode(false)
                    .HasComment("CMS table name associated with page");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.PageMaster)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageMaster_ModuleMaster");
            });

            modelBuilder.Entity<PermissionMaster>(entity =>
            {
                entity.HasKey(e => e.PermissionId);

                entity.Property(e => e.PermissionId).HasComment("Unique id assoicated with each permission");

                entity.Property(e => e.IsAdd).HasComment("Permission flag for add any data");

                entity.Property(e => e.IsDelete).HasComment("Permission flag for delete any data");

                entity.Property(e => e.IsEdit).HasComment("Permission flag for edit any data");

                entity.Property(e => e.IsView).HasComment("Permission flag for view any data");

                entity.Property(e => e.PageId).HasComment("PageId associated with current permission");

                entity.Property(e => e.RoleId).HasComment("RoleId associated with each permission");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PermissionMaster)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionMaster_PageMaster");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PermissionMaster)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionMaster_RoleMaster");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_MstRoles");

                entity.Property(e => e.RoleId).HasComment("Unique id assoicated with each role");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(role is in use) And isactive=0(role is soft deleted & no more in use)");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Unique role name");
            });

            modelBuilder.Entity<ServiceBookingMaster>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceId).HasComment("Unique id assoicated with each service booking");

                entity.Property(e => e.CentreId).HasComment("ServicCentreId where customer booked service");

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Cutsomer's contact number");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Service booking created date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Cutsomer's email address");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Cutsomer's full name");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(service booking is in use) And isactive=0(service booking is soft deleted & no more in use)");

                entity.Property(e => e.ModelId).HasComment("ModelId for which customer booked service");

                entity.Property(e => e.Remark)
                    .IsUnicode(false)
                    .HasComment("Service booking remarks ");

                entity.Property(e => e.ServiceDate)
                    .HasColumnType("date")
                    .HasComment("Date when customer wants to book service");

                entity.Property(e => e.ServiceTime).HasComment("Time when customer wants to book service");

                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Type of sevice for which user made service booking");

                entity.Property(e => e.ServiceTypeId).HasComment("SeviceTypeId for which user made service booking");

                entity.Property(e => e.Status).HasComment("To maintain service booking status like( Pending,Waiting,Expired etc...)");

                entity.Property(e => e.TimeSlotId).HasComment("TimeSlotId when customer wants to book service");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Cutsomer's salutation who is booking service");

                entity.Property(e => e.VehModelNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's vehicle model number");

                entity.Property(e => e.VehRegNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's vehicle registration number");

                entity.HasOne(d => d.Centre)
                    .WithMany(p => p.ServiceBookingMaster)
                    .HasForeignKey(d => d.CentreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceBookingMaster_ServiceCentreMaster");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ServiceBookingMaster)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_ServiceBookingMaster_ModelMaster");

                entity.HasOne(d => d.ServiceTypeNavigation)
                    .WithMany(p => p.ServiceBookingMaster)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_ServiceBookingMaster_ServiceTypeMaster");

                entity.HasOne(d => d.TimeSlot)
                    .WithMany(p => p.ServiceBookingMaster)
                    .HasForeignKey(d => d.TimeSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceBookingMaster_TimeSlots");
            });

            modelBuilder.Entity<ServiceCentreMaster>(entity =>
            {
                entity.HasKey(e => e.CentreId);

                entity.Property(e => e.CentreId).HasComment("Unique id assoicated with each service centre");

                entity.Property(e => e.Address)
                    .IsUnicode(false)
                    .HasComment("Service centre address");

                entity.Property(e => e.BlockDays)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Weekdays when service centre closed for booking");

                entity.Property(e => e.CentreName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Unique service centre name");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Service centre email address");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(service centre is in use) And isactive=0(service centre is soft deleted & no more in use)");

                entity.Property(e => e.Phone)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Service centre phone number");

                entity.Property(e => e.SortOrder).HasComment("Service centre sort order");
            });

            modelBuilder.Entity<ServiceTypeMaster>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasComment("Unique id assoicated with each service type");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(service type is in use) And isactive=0(service type is soft deleted & no more in use)");

                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Unique service type value");

                entity.Property(e => e.SortOrder).HasComment("Service type sort order");
            });

            modelBuilder.Entity<SettingMaster>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.Property(e => e.SettingId).HasComment("Unique id assoicated with each setting record");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(setting tag is in use) And isactive=0(setting tag is soft deleted & no more in use)");

                entity.Property(e => e.IsEditable).HasComment("IsEditable= 0 :- tag is not editable AND IsEditable= 1 :- tag is editable");

                entity.Property(e => e.RelatedValue)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Related value of tag");

                entity.Property(e => e.TagCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("A tag code to classify different tagnames ");

                entity.Property(e => e.TagName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Tag name");

                entity.Property(e => e.TagValue)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("the value for tag");
            });

            modelBuilder.Entity<SpotLightMaster>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Unique id associated with spotlight");

                entity.Property(e => e.BtnText)
                    .IsRequired()
                    .HasColumnName("btnText")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Text of button displayed on spotlight image");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasComment("Spotlight description");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(spotlight is in use) And isactive=0(spotlight is soft deleted & no more in use)");

                entity.Property(e => e.IsSpotLight).HasComment("On Frontend home page Spotlight image on the head of title (IsSpotLight = 1) will be displayed AND (IsSpotLight = 0) : will not be displayed.");

                entity.Property(e => e.LnkName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Linkext of button displayed on spotlight image");

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Spotlight image file name");

                entity.Property(e => e.RedirectUrl)
                    .IsRequired()
                    .HasColumnName("RedirectURL")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("On clicking link name user should redirect to this url ");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Spotlight title");
            });

            modelBuilder.Entity<TimeSlots>(entity =>
            {
                entity.HasKey(e => e.TimeId);

                entity.Property(e => e.TimeId).HasComment("Unique id assoicated with each time slot");

                entity.Property(e => e.Capacity).HasComment("Max no of service booking allowed for specific time slot.");

                entity.Property(e => e.CentreId).HasComment("Service centre id associated with time slot");

                entity.Property(e => e.Interval).HasComment("No of timeslots (Interval measurement in MINUTES) to be created between from time and to time for from date to to date.");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(timeslot is in use) And isactive=0(timeslot is soft deleted & no more in use)");

                entity.Property(e => e.ScheduleDate)
                    .HasColumnType("date")
                    .HasComment("Timeslot scheduled date");

                entity.Property(e => e.ScheduleTime).HasComment("Timeslot scheduled time");

                entity.HasOne(d => d.Centre)
                    .WithMany(p => p.TimeSlots)
                    .HasForeignKey(d => d.CentreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeSlots_ServiceCentreMaster");
            });

            modelBuilder.Entity<UserLoginHistory>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Unique id assoicated with each user login record");

                entity.Property(e => e.Browser)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Browser name from where user logged in");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Login user ip address");

                entity.Property(e => e.OnDate)
                    .HasColumnName("onDate")
                    .HasColumnType("datetime")
                    .HasComment("User login date");

                entity.Property(e => e.UserId).HasComment("Login userid");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasComment("Unique id assoicated with each user");

                entity.Property(e => e.CreatedBy).HasComment("UserId who created new user");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("User created date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("User email address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("User first name");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(user is active) And isactive=0(user is deactivated)");

                entity.Property(e => e.IsDelete).HasComment("IsDelete=0(user is not deleted) And IsDelete=1(user is soft deleted and no more in use)");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("User last name");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("User mobile number");

                entity.Property(e => e.ModifyBy).HasComment("UserId who modified user detail");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasComment("Date when user detail modified");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("User password");

                entity.Property(e => e.RoleId).HasComment("RoleId associated with user");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Unique Username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserMaster)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMaster_MstRoles");
            });

            modelBuilder.Entity<VerificationCodes>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Unique id assoicated with each verification code");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Six digit numeric code attached in reset password link and sent in email to user");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("Verification code created date");

                entity.Property(e => e.IsUsed).HasComment("IsUsed=0(verification code is not used) And IsUsed=1(verification code is used)");

                entity.Property(e => e.UserId).HasComment("UserId for whom verification code generated and sent in email for reset password link");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VerificationCodes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VerificationCodes_UserMaster");
            });

            modelBuilder.Entity<WarrantyCms>(entity =>
            {
                entity.ToTable("WarrantyCMS");

                entity.Property(e => e.Id).HasComment("Unique id assoicated with WarrantyCMS record");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasComment("WarrantyCMS description");

                entity.Property(e => e.IsActive).HasComment("Isactive=0(WarrantyCMS is active and in use) And isactive=1(WarrantyCMS is soft deleted & no more in use)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("WarrantyCMS image file name");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("WarrantyCMS title");
            });

            modelBuilder.Entity<WarrantyMaster>(entity =>
            {
                entity.HasKey(e => e.WarrantyId);

                entity.Property(e => e.WarrantyId).HasComment("Unique id associated with warranty ");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Customer's address");

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer's contatct number");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Warranty created date");

                entity.Property(e => e.DealerId).HasComment("Customer's dealer id");

                entity.Property(e => e.DealerName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Customer's dealer name");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Customer's email address");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's first name");

                entity.Property(e => e.IsActive).HasComment("Isactive=1(warranty is in use) And isactive=0(warranty is soft deleted & no more in use)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's last name");

                entity.Property(e => e.ModelId).HasComment("Customer's vehicle model id");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Customer's vehicle model name");

                entity.Property(e => e.ModelNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's vehicle model number");

                entity.Property(e => e.Newsletter)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer's wish to be contactd or not by Mail,Email Or Phone");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer's  postal code");

                entity.Property(e => e.PurchaseProof)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's purcahse proof file name (pdf or image file)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer's salutaion id ");

                entity.Property(e => e.VehChassisNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's vehicle chasis number");

                entity.Property(e => e.VehFrameNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Customer's vehicle frame number");

                entity.Property(e => e.VehRegDate)
                    .HasColumnType("date")
                    .HasComment("Customer's vehicle registration date");

                entity.Property(e => e.VehRegNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer's vehicle registration number");

                entity.HasOne(d => d.Dealer)
                    .WithMany(p => p.WarrantyMaster)
                    .HasForeignKey(d => d.DealerId)
                    .HasConstraintName("FK_WarrantyMaster_DealerMaster");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.WarrantyMaster)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarrantyMaster_ModelMaster");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
