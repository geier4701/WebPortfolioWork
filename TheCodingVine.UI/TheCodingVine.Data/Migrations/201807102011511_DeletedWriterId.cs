namespace TheCodingVine.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedWriterId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BlogPosts", name: "BlogWriterId", newName: "BlogWriter_Id");
            RenameIndex(table: "dbo.BlogPosts", name: "IX_BlogWriterId", newName: "IX_BlogWriter_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BlogPosts", name: "IX_BlogWriter_Id", newName: "IX_BlogWriterId");
            RenameColumn(table: "dbo.BlogPosts", name: "BlogWriter_Id", newName: "BlogWriterId");
        }
    }
}
