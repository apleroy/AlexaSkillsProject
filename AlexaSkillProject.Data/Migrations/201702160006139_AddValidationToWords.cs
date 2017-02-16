namespace AlexaSkillProject.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationToWords : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Words", "WordName", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.Words", "PartOfSpeech", c => c.String(nullable: false));
            AlterColumn("dbo.Words", "Definition", c => c.String(nullable: false));
            AlterColumn("dbo.Words", "Example", c => c.String(nullable: false));
            CreateIndex("dbo.Words", "WordName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Words", new[] { "WordName" });
            AlterColumn("dbo.Words", "Example", c => c.String());
            AlterColumn("dbo.Words", "Definition", c => c.String());
            AlterColumn("dbo.Words", "PartOfSpeech", c => c.String());
            AlterColumn("dbo.Words", "WordName", c => c.String());
        }
    }
}
