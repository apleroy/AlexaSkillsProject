namespace AlexaSkillProject.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlexaMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlexaUserId = c.String(),
                        RequestCount = c.Int(nullable: false),
                        LastRequestDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AlexaRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlexaMemberId = c.Int(nullable: false),
                        SessionId = c.String(),
                        AppId = c.String(),
                        RequestId = c.String(),
                        UserId = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Intent = c.String(),
                        Slots = c.String(),
                        IsNew = c.Boolean(nullable: false),
                        Version = c.String(),
                        Type = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlexaMembers", t => t.AlexaMemberId, cascadeDelete: true)
                .Index(t => t.AlexaMemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlexaRequests", "AlexaMemberId", "dbo.AlexaMembers");
            DropIndex("dbo.AlexaRequests", new[] { "AlexaMemberId" });
            DropTable("dbo.AlexaRequests");
            DropTable("dbo.AlexaMembers");
        }
    }
}
