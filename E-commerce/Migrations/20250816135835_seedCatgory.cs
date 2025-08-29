using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Migrations
{
    /// <inheritdoc />
    public partial class seedCatgory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Catgories (Name, Description, Status) values ('Mobile', 'Phasellus in felis. Donec semper sapien a libero. Nam dui.', 1); insert into Catgories (Name, Description, Status)" +
                " values ('Labptop', 'Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.  Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est.', 1); " +
                "insert into Catgories (Name, Description, Status) values ('Camera', 'Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.  Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.', 0); " +
                "insert into Catgories (Name, Description, Status) values ('Tablets', 'Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.  Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.  Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.', 1); " +
                "insert into Catgories (Name, Description, Status) values ('Acssecories', 'Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.  Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.', 1); ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Catgories");
        }
    }
}
