namespace SuperMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TipoProductoes", "Sucursal_SucursalId", "dbo.Sucursals");
            DropIndex("dbo.TipoProductoes", new[] { "Sucursal_SucursalId" });
            RenameColumn(table: "dbo.TipoProductoes", name: "Sucursal_SucursalId", newName: "SucursalId");
            AddForeignKey("dbo.TipoProductoes", "SucursalId", "dbo.Sucursals", "SucursalId");
            CreateIndex("dbo.TipoProductoes", "SucursalId");
            DropColumn("dbo.TipoProductoes", "SucursualId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoProductoes", "SucursualId", c => c.Int(nullable: false));
            DropIndex("dbo.TipoProductoes", new[] { "SucursalId" });
            DropForeignKey("dbo.TipoProductoes", "SucursalId", "dbo.Sucursals");
            RenameColumn(table: "dbo.TipoProductoes", name: "SucursalId", newName: "Sucursal_SucursalId");
            CreateIndex("dbo.TipoProductoes", "Sucursal_SucursalId");
            AddForeignKey("dbo.TipoProductoes", "Sucursal_SucursalId", "dbo.Sucursals", "SucursalId");
        }
    }
}
