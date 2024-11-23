namespace OnionArchitectureApp.Application.Parameters;

public record OrderByRequestDto(string ColumnName, bool Ascending = true);
