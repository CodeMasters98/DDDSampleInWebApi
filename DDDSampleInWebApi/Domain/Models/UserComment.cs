using DDDSampleInWebApi.Domain.Common;

namespace DDDSampleInWebApi.Domain.Models
{
    public class UserComment : Entity
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }

    public class UserCommentSevice
    {
        public void Execute()
        {
            //Business
            //SP -> Transaction
            // Active Record
        }
    }
}

