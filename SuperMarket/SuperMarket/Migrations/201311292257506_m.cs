namespace SuperMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.TipoProductoes",
                c => new
                    {
                        TipoProductoId = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                    })
                .PrimaryKey(t => t.TipoProductoId);
            
            CreateTable(
                "dbo.Suplidors",
                c => new
                    {
                        SuplidorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.SuplidorId);
            
            CreateTable(
                "dbo.Sucursals",
                c => new
                    {
                        SucursalId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.SucursalId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Foto = c.String(),
                        TipoProductoID = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        SuplidorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoProductoes", t => t.TipoProductoID, cascadeDelete: true)
                .ForeignKey("dbo.Sucursals", t => t.SucursalId, cascadeDelete: true)
                .ForeignKey("dbo.Suplidors", t => t.SuplidorId, cascadeDelete: true)
                .Index(t => t.TipoProductoID)
                .Index(t => t.SucursalId)
                .Index(t => t.SuplidorId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Productoes", new[] { "SuplidorId" });
            DropIndex("dbo.Productoes", new[] { "SucursalId" });
            DropIndex("dbo.Productoes", new[] { "TipoProductoID" });
            DropForeignKey("dbo.Productoes", "SuplidorId", "dbo.Suplidors");
            DropForeignKey("dbo.Productoes", "SucursalId", "dbo.Sucursals");
            DropForeignKey("dbo.Productoes", "TipoProductoID", "dbo.TipoProductoes");
            DropTable("dbo.Productoes");
            DropTable("dbo.Sucursals");
            DropTable("dbo.Suplidors");
            DropTable("dbo.TipoProductoes");
            DropTable("dbo.UserProfile");
        }
    }
}
