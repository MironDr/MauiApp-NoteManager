using System.ComponentModel;
using MauiApp1.Base;
using SQLite;

namespace MauiApp1.Models;

public class BaseModel : BaseCommon
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
}