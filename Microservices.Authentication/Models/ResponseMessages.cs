using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Authentication.Models
{
    public class ResponseMessages
    {
        public static  string Successful = "Completed Successfully";
        public static string Failed = "Operation Failed";
        public static string EmailNotAvailable = "Email is not available for use.";
        public static string EmailAlreadyExistError = "The provided is email is already in use. Kindly login.";
        public static string NotFoundError = "Record not found.";
    }
}
