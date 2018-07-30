namespace TheCodingVine.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        BlogPostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        PostDate = c.DateTime(),
                        RemoveDate = c.DateTime(),
                        IsApproved = c.Boolean(nullable: false),
                        BlogNotes = c.String(),
                        BlogWriterId = c.String(maxLength: 128),
                        TagInputs = c.String(),
                    })
                .PrimaryKey(t => t.BlogPostId)
                .ForeignKey("dbo.AspNetUsers", t => t.BlogWriterId)
                .Index(t => t.BlogWriterId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SearchTags",
                c => new
                    {
                        SearchTagId = c.Int(nullable: false, identity: true),
                        SearchTagBody = c.String(),
                    })
                .PrimaryKey(t => t.SearchTagId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StaticPosts",
                c => new
                    {
                        StaticPostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.StaticPostId);
            
            CreateTable(
                "dbo.SearchTagBlogPosts",
                c => new
                    {
                        SearchTag_SearchTagId = c.Int(nullable: false),
                        BlogPost_BlogPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SearchTag_SearchTagId, t.BlogPost_BlogPostId })
                .ForeignKey("dbo.SearchTags", t => t.SearchTag_SearchTagId, cascadeDelete: true)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_BlogPostId, cascadeDelete: true)
                .Index(t => t.SearchTag_SearchTagId)
                .Index(t => t.BlogPost_BlogPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SearchTagBlogPosts", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.SearchTagBlogPosts", "SearchTag_SearchTagId", "dbo.SearchTags");
            DropForeignKey("dbo.BlogPosts", "BlogWriterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SearchTagBlogPosts", new[] { "BlogPost_BlogPostId" });
            DropIndex("dbo.SearchTagBlogPosts", new[] { "SearchTag_SearchTagId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BlogPosts", new[] { "BlogWriterId" });
            DropTable("dbo.SearchTagBlogPosts");
            DropTable("dbo.StaticPosts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SearchTags");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BlogPosts");
        }
    }
}
