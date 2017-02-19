namespace AlexaSkillProject.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefinitionsToWords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Words", "PartOfSpeech", c => c.String());
            AddColumn("dbo.Words", "Definition", c => c.String());
            AddColumn("dbo.Words", "Example", c => c.String());
            DropColumn("dbo.Words", "WordOfTheDayDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Words", "WordOfTheDayDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Words", "Example");
            DropColumn("dbo.Words", "Definition");
            DropColumn("dbo.Words", "PartOfSpeech");
        }
    }
}
