namespace Bookify.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilePathsToBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "CoverImagePath", c => c.String());
            AddColumn("dbo.Books", "FilePath", c => c.String());
            DropColumn("dbo.Books", "ISBN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "ISBN", c => c.String());
            DropColumn("dbo.Books", "FilePath");
            DropColumn("dbo.Books", "CoverImagePath");
        }
    }
}
