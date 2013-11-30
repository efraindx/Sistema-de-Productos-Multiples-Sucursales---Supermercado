namespace SuperMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoProductoes", "SucursualId", c => c.Int(nullable: false));
            AddColumn("dbo.TipoProductoes", "Sucursal_SucursalId", c => c.Int());
            AddForeignKey("dbo.TipoProductoes", "Sucursal_SucursalId", "dbo.Sucursals", "SucursalId");
            CreateIndex("dbo.TipoProductoes", "Sucursal_SucursalId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TipoProductoes", new[] { "Sucursal_SucursalId" });
            DropForeignKey("dbo.TipoProductoes", "Sucursal_SucursalId", "dbo.Sucursals");
            DropColumn("dbo.TipoProductoes", "Sucursal_SucursalId");
            DropColumn("dbo.TipoProductoes", "SucursualId");
        }
    }
}
