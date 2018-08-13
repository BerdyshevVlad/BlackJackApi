namespace BlackJack.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Round : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionMessage = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Int(nullable: false),
                        WinsNumbers = c.Int(nullable: false),
                        Status = c.String(),
                        PlayerType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardPlayers",
                c => new
                    {
                        Card_Id = c.Int(nullable: false),
                        Player_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Card_Id, t.Player_Id })
                .ForeignKey("dbo.Cards", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Card_Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.RoundPlayers",
                c => new
                    {
                        Round_Id = c.Int(nullable: false),
                        Player_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Round_Id, t.Player_Id })
                .ForeignKey("dbo.Rounds", t => t.Round_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Round_Id)
                .Index(t => t.Player_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoundPlayers", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.RoundPlayers", "Round_Id", "dbo.Rounds");
            DropForeignKey("dbo.CardPlayers", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.CardPlayers", "Card_Id", "dbo.Cards");
            DropIndex("dbo.RoundPlayers", new[] { "Player_Id" });
            DropIndex("dbo.RoundPlayers", new[] { "Round_Id" });
            DropIndex("dbo.CardPlayers", new[] { "Player_Id" });
            DropIndex("dbo.CardPlayers", new[] { "Card_Id" });
            DropTable("dbo.RoundPlayers");
            DropTable("dbo.CardPlayers");
            DropTable("dbo.Rounds");
            DropTable("dbo.Cards");
            DropTable("dbo.Players");
            DropTable("dbo.ExceptionDetails");
        }
    }
}
