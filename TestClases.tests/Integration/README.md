# Integration tests

These tests call the real MySQL repositories and stored procedures. They only run when `GRANDT_TEST_CONNECTION_STRING` is set; otherwise xUnit skips them.

Use a dedicated test database with the GrandDT schema and stored procedures installed. Do not point this variable at a database whose contents must be preserved.

```powershell
$env:GRANDT_TEST_CONNECTION_STRING = "server=localhost;database=GranDT_Test;uid=your_test_user;pwd=your_test_password;port=3306;"
dotnet test "TestClases.tests/TestClases.tests.csproj" --filter "Category=Integration"
Remove-Item Env:GRANDT_TEST_CONNECTION_STRING
```

The integration tests currently use read-only `ObtenerTodos` procedures.