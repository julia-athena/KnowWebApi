using System.ComponentModel;

namespace DataAccess.Model;

public enum PostState
{
    [Description("Черновик")] 
    Draft,
    [Description("На проверке")]
    UnderReview,
    [Description("Опубликовано")] 
    Published
}