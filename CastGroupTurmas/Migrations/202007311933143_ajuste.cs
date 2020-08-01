namespace CastGroupTurmas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajuste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CURSO", "Categoria_Id", "dbo.Categoria");
            DropIndex("dbo.Curso", new[] { "Categoria_Id" });
            RenameColumn(table: "dbo.Curso", name: "Categoria_Id", newName: "CategoriaId");
            AlterColumn("dbo.Curso", "CategoriaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Curso", "CategoriaId");
            AddForeignKey("dbo.Curso", "CategoriaId", "dbo.Categoria", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Curso", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Curso", new[] { "CategoriaId" });
            AlterColumn("dbo.Curso", "CategoriaId", c => c.Int());
            RenameColumn(table: "dbo.Curso", name: "CategoriaId", newName: "Categoria_Id");
            CreateIndex("dbo.Curso", "Categoria_Id");
            AddForeignKey("dbo.CURSO", "Categoria_Id", "dbo.Categoria", "Id");
        }
    }
}
