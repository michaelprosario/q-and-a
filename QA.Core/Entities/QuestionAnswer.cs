using DocumentStore.Core.Interfaces;
using System;
using System.Runtime.Serialization;

namespace QA.Core.Entities
{
    [DataContract]
    public class QuestionAnswer : Post
    {
        [DataMember]
        public int Votes { get; set; }
        [DataMember]
        public string QuestionId { get; set; }
    }
}