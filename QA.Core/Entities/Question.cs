using DocumentStore.Core.Interfaces;
using System;
using System.Runtime.Serialization;

namespace QA.Core.Entities
{
    [DataContract]
    public class Question : Post
    {
        [DataMember]
        public int Votes { get; set; }
        [DataMember]
        public int Views { get; set; }
        [DataMember]
        public int AnswerCount { get; set; }
    }
}