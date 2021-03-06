﻿using AgentLibrary;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{
    [DataContract]
    public class MovieInformationItem : DialogueItem
    {
        //private OutputAction outputAction = new OutputAction();
        private List<string> inputQueryTagList;
        //private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        public MovieInformationItem() { }

        public MovieInformationItem(string id, List<string> inputQueryTagList, string successTargetContext, string successTargetID, string failureTargetContext, string failureTargetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string queryTag = inputQueryTagList[0]; //TODO maybe not needed or change GetLastStringByTag
            string movieTitle = "";
            string movieInformationString = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                movieTitle = (string)itemSought.GetContent();
            }

            var _ultraManager = UltraManager.Instance;
            Boolean existingMovie = false;
            foreach (var movie in _ultraManager.MovieList)
            {
                if (movie.Title.ToLower() == movieTitle)
                {
                    existingMovie = true;
                    movieInformationString = movie.Title + " is a " + movie.Genre + " from " + movie.Year + " that has a imdb rating of " + movie.ImdbRating;
                }
            }

            if (existingMovie)
            {
                ownerAgent.SendSpeechOutput(movieInformationString);
                targetContext = successTargetContext;
                targetID = successTargetID;
            }
            else
            {
                targetContext = failureTargetContext;
                targetID = failureTargetID;

            }
            return true;
        }

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }
    }
}
