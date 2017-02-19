namespace AlexaSkillProject.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWordOfTheDayDateToWords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Words", "WordOfTheDayDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Words", "WordOfTheDayDate");
        }
    }
}
