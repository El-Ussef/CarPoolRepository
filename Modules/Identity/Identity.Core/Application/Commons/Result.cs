using System.Globalization;

namespace Identity.Core.Application.Commons;

public class Result
{
    private static readonly Result _success = new Result { Succeeded = true };
    private readonly List<Error> _errors = new List<Error>();
    public bool Succeeded { get; protected set; }
    
    public IEnumerable<Error> Errors => _errors;
    
    public static Result Success => _success;
    
    public static Result Failed(params Error[] errors)
    {
        var result = new Result { Succeeded = false };
        if (errors != null)
        {
            result._errors.AddRange(errors);
        }
        return result;
    }

    internal static Result Failed(List<Error>? errors)
    {
        var result = new Result { Succeeded = false };
        if (errors != null)
        {
            result._errors.AddRange(errors);
        }
        return result;
    }
    
    public override string ToString()
    {
        return Succeeded ?
               "Succeeded" :
               string.Format(CultureInfo.InvariantCulture, "{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Code).ToList()));
    }
}

public class Error
{
    
    public string Code { get; set; } = default!;
    public string Description { get; set; } = default!;
}