Imports MySqlConnector

Public Class Dao

    'Private Shared Sub DataSetSample(connectionString As String)
    '    Dim connection As SqlConnection
    '    Using (connection As SqlConnection = New SqlConnection())

    'connection.ConnectionString = connectionString;
    'connection.Open();

    '// DataSetによる参照、更新
    'String selectQuery = "SELECT id, name, age FROM member";
    'String updateQuery = "UPDATE member SET age = @age WHERE id = @id";

    'SqlDataAdapter adapter = New SqlDataAdapter();

    '// 主キー情報をDBから取得
    'adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

    '// クエリの設定
    'adapter.SelectCommand = New SqlCommand(selectQuery, connection);
    'adapter.UpdateCommand = New SqlCommand(updateQuery, connection);

    '// Updateパラメータの設定
    'SqlParameter ageParam = New SqlParameter();
    'ageParam.ParameterName = "@age";
    'ageParam.SourceColumn = "age";
    'adapter.UpdateCommand.Parameters.Add(ageParam);
    'SqlParameter idParam = New SqlParameter();
    'idParam.ParameterName = "@id";
    'idParam.SourceColumn = "id";
    'adapter.UpdateCommand.Parameters.Add(idParam);

    'DataSet DataSet = New DataSet();
    'adapter.Fill(DataSet, "memberTable");
    'DataTable DataTable = DataSet.Tables["memberTable"];

    '// データの参照
    'foreach(DataRow row In datatable.Rows)
    '{
    '    Console.WriteLine("id={0},name={1},age={2}", row["id"], row["name"], row["age"]);
    '}

    '// データの更新
    'DataRow carolRow = DataTable.Rows.Find(3);
    'carolRow["age"] = 50;

    'adapter.Update(DataSet, "memberTable")
    'End Using

    'End Sub

End Class
