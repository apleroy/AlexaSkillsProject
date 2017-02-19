namespace AlexaSkillProject.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWordOfTheDayToWords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Words", "WordOfTheDay", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Words", "WordOfTheDay");
        }
    }
}
