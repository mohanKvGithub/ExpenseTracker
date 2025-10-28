using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;


namespace TCW.Utility;

public class UtilsService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    #region Static Property
    public string[] Roles => new string[] { "Operator", "Master User", "Supervisor", "Brand Manager", "Business Analyst", "Community Manager" };
    public static string[] FabOrderStatus => new string[] { "Bottles Available", "Bottles InProgress", "Bottles Finished", "Boxes InProgress", "Boxes Finished" };
    #endregion


    public UtilsService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public int CurrentUserId
    {
        get
        {
                var claims = httpContextAccessor.HttpContext.User.Claims;
                var uId = claims.Where(p => p.Type == "UserId").Select(p => p.Value).SingleOrDefault();
                _ = int.TryParse(uId, out int userId);

                return userId;
        }
    }

    #region REGEX
    //public const string REGEX_Password = "(?=^.{8,15}$)((?!.*\\s)(?=.*[A-Z])(?=.*[a-z])(?=(.*\\d){1,}))((?!.*[\",;&| '])|(?=(.*\\W){1,}))(?!.*[\",;&|'])^.*$";
    public const string REGEX_Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,20}";
    // Valid Numbers 0123456789  012-345-6789 (012)-345-6789
    // Ref: http://refiddle.com/refiddles/validate-phone-number
    // Orginal Regex: ^(\+?( |-|\.)?\d{1,2}( |-|\.)?)?(\(?\d{3}\)?|\d{3})( |-|\.)?(\d{3}( |-|\.)?\d{4})$
    // Updated Regex to allow india format no: ^(\+?( |-|\.)?\d{1,2}( |-|\.)?)?(\(?\d{2,3}\)?|\d{3})( |-|\.)?(\d{3,5}( |-|\.)?\d{4,5})$
    //public const string REGEX_ContactNumber = @"^(\+?( |-|\.)?\d{1,2}( |-|\.)?)?(\(?\d{2,3}\)?|\d{3})( |-|\.)?(\d{3,5}( |-|\.)?\d{4,5})$";
    // Updated on 9 May 2019 for only given format +1 8866351488 or +91 8866351488 or +918866351488
    public const string REGEX_ContactNumber = @"^(\+?( |-|\.)?\d{1,2}( |-|\.)?)?(\(?\d{3}\)?|\d{3})( |-|\.)?(\d{3}( |-|\.)?\d{4})$";
    public const string Msg_ContactNumber = "Phone number is invalid";

    //public const string Msg_Password_Validation = "Password must be 8-15 characters long with one capital letter, one lower case letter and one number. Special characters \",;&|' are not allowed";
    public const string Msg_Password_Validation = "Password must have contained at least 6 characters with alpha-numeric and capital and non-capital";
    public const string Msg_Password_Required = "Password is required";
    public const string Msg_Email_Required = "Email is required";
    #endregion
}

public enum Roles
{
    SuperAdmin,
    Admin,
    Customer,
    Store
}

public enum RewardStatus
{
    Pending,
    Completed,
    Rejected
}

public enum APIStatus
{
    Fail = 0,
    Success = 1
}

public static class APIMessages
{
    public static string OkMessge => "Operation completed successfully";
    public static string ExceptionMessge => "Something went wrong! Contact Administrator";
    public static string NoRecordsMessge => "No records";
    public static string AlreadyExistMessge => "Data with same name already exist";
    public static string NotFoundMessage => "Record not found";
    public static string InvalidInputMessge => "Invalid input";
    public static string FailedMessge => "Operation failed";
    public static string AccessDeniedMessge => "Access denied";

    public static string AlreadyExistCustomMessge(string msg = "Record ")
    {
        return string.Format("{0} already exist", msg);
    }
}