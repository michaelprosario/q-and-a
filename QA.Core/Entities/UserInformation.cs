using System.Runtime.Serialization;

namespace QA.Core.Entities
{
  [DataContract]
  public class UserInformation
  {
    [DataMember]

    public string UserName { get; set; }
  }
}