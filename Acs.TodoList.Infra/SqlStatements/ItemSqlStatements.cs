using Acs.TodoList.Domain.Entities;

namespace Acs.TodoList.Infra.SqlStatements
{
    public static class ItemSqlStatements
    {
        public static string InsertItem => @$"
            INSERT INTO ""Item""
            (id, title, description, status, favorite)
            VALUES(:{nameof(Item.Id)}, :{nameof(Item.Title)}, :{nameof(Item.Description)}, :{nameof(Item.Status)}, :{nameof(Item.Favorite)});
            ";

        public static string GetItemWithPagination => @"
            select count(*) TotalItems, :limit as Limit, :offset as Offset from ""Item"";
            SELECT * FROM ""Item"" LIMIT :limit OFFSET :offset;
            ";

        public static string GetById => @"select * from ""Item"" where id = :id";

        public static string UpdateItem => @$"
            UPDATE ""Item""
            SET title=:{nameof(Item.Title)}, description=:{nameof(Item.Description)}, status=:{nameof(Item.Status)}, favorite=:{nameof(Item.Favorite)}
            WHERE id=:{nameof(Item.Id)};
            ";
    }
}
