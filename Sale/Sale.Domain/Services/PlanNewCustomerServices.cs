namespace Sale.Domain.Services;

public static class PlanNewCustomerServices
{
    public static string GenerateCustomerCode(string cityName, string districtName, int count)
    {
        string ct = string.Join("", cityName.Split(' ').Select(word => word[0])).ToUpper();
        string dt = string.Join("", districtName.Split(' ').Select(word => word[0])).ToUpper();
        return $"MM-{ct}-{dt}-{count + 1}";
    }

    public static string GenerateCustomerName(string cityName, string districtName, int count)
    {
        return $"MM-{cityName}-{districtName}-{count + 1}";
    }
}