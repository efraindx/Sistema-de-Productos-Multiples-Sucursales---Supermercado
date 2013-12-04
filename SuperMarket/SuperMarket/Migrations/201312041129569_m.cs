namespace SuperMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MarcaProductoes",
                c => new
                    {
                        MarcaProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.MarcaProductoId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Foto = c.String(),
                        TipoProductoID = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        SuplidorId = c.Int(nullable: false),
                        MarcaProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarcaProductoes", t => t.MarcaProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Sucursals", t => t.SucursalId, cascadeDelete: true)
                .ForeignKey("dbo.Suplidors", t => t.SuplidorId, cascadeDelete: true)
                .ForeignKey("dbo.TipoProductoes", t => t.TipoProductoID, cascadeDelete: true)
                .Index(t => t.MarcaProductoId)
                .Index(t => t.SucursalId)
                .Index(t => t.SuplidorId)
                .Index(t => t.TipoProductoID);
            
            CreateTable(
                "dbo.Sucursals",
                c => new
                    {
                        SucursalId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.SucursalId);
            
            CreateTable(
                "dbo.Suplidors",
                c => new
                    {
                        SuplidorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.SuplidorId);
            
            CreateTable(
                "dbo.TipoProductoes",
                c => new
                    {
                        TipoProductoId = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                        SucursalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoProductoId)
                .ForeignKey("dbo.Sucursals", t => t.SucursalId)
                .Index(t => t.SucursalId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productoes", "TipoProductoID", "dbo.TipoProductoes");
            DropForeignKey("dbo.TipoProductoes", "SucursalId", "dbo.Sucursals");
            DropForeignKey("dbo.Productoes", "SuplidorId", "dbo.Suplidors");
            DropForeignKey("dbo.Productoes", "SucursalId", "dbo.Sucursals");
            DropForeignKey("dbo.Productoes", "MarcaProductoId", "dbo.MarcaProductoes");
            DropIndex("dbo.Productoes", new[] { "TipoProductoID" });
            DropIndex("dbo.TipoProductoes", new[] { "SucursalId" });
            DropIndex("dbo.Productoes", new[] { "SuplidorId" });
            DropIndex("dbo.Productoes", new[] { "SucursalId" });
            DropIndex("dbo.Productoes", new[] { "MarcaProductoId" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.TipoProductoes");
            DropTable("dbo.Suplidors");
            DropTable("dbo.Sucursals");
            DropTable("dbo.Productoes");
            DropTable("dbo.MarcaProductoes");
        }
    }
}
