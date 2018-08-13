using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgentLibrary.Memories;

namespace AgentLibrary
{
    [DataContract]
    // A pattern defines a list of strings that can be matched to a given input as follows:
    //
    // Before any matching is attempted, "," and ";" are removed, and
    // all words a changed to lowercase.
    //
    // [   ] defines alternative choices
    // Example: We [can will might] go, matches "we can go", "we will go" and "we might go"
    //
    // ![   ] defines excluded words (at the position in question)
    // Example We will ![not] go. This pattern would match "we will probably go" but not "we will not go"
    // Note that it would NOT match "we will go", since the pattern match is checked position by position.
    // Note also that patterns are processed in the order they are defined. Thus, if the two patterns
    //
    // We will go
    // We will ![not] go
    //
    // Are used, a match would be obtained for "we will go", "we will surely go" etc. but not "we will not go".
    // Note that the [ ] are required, even if there is only one word in between.
    //
    // * defines a single wild-card.
    // Example: We will * go: This would match " we will surely go" but ALSO "we will not go". Thus, some
    // care is required when using wild cards.
    //
    // Note that a wild-card at the END of a pattern means that anything may follow after the wild card.
    // Thus
    //
    // We will go * matches "We will go today", "We will go today or tomorrow" etc. (but NOT "we will go").
    // 
    // <..> defines a query item, e.g. <Q1>, <Q2> etc.
    public class Pattern
    {
        private string definition;
        private List<string> processedDefinitionList;
        private List<PatternItem> patternItemList;
        private char wildCardCharacter = AgentConstants.WILD_CARD_CHARACTER;
        private char negationCharacter = AgentConstants.NEGATION_CHARACTER;
        private char queryStartCharacter = AgentConstants.QUERY_START_CHARACTER;
        private char queryEndCharacter = AgentConstants.QUERY_END_CHARACTER;
        private char exactExpressionStartCharacter = AgentConstants.EXACT_EXPRESSION_START_CHARACTER;
        private char exactExpressionEndCharacter = AgentConstants.EXACT_EXPRESSION_END_CHARACTER;
        private char optionsStartCharacter = AgentConstants.OPTIONS_START_CHARACTER;
        private char optionsEndCharacter = AgentConstants.OPTIONS_END_CHARACTER;

        private List<string> queryTerms = new List<string>();

        public Pattern(string definition)
        {
            this.definition = definition;
        }

        public Pattern() { }

        public Boolean ProcessDefinition()
        {
            // First, split the definition string
            processedDefinitionList = new List<string>();
            string definitionItem = "";

            string processedDefinition = definition;
            while (processedDefinition.Length > 0)
            {
                if (processedDefinition[0] == optionsStartCharacter)
                {
                    int nextIndex = processedDefinition.IndexOf(optionsEndCharacter);
                    if (nextIndex < 0)
                    {
                        return false; // Unbalanced "["
                    }
                    else
                    {
                        definitionItem = processedDefinition.Substring(0, nextIndex + 1);
                        processedDefinitionList.Add(definitionItem);
                        processedDefinition = processedDefinition.Remove(0, definitionItem.Length);
                        definitionItem = "";
                    }
                }
                else if (processedDefinition[0] == wildCardCharacter)
                {
                    definitionItem = wildCardCharacter.ToString();
                    processedDefinitionList.Add(definitionItem);
                    definitionItem = "";
                    if (processedDefinition.Length > 1)  // Otherwise: done
                    {
                        processedDefinition = processedDefinition.Remove(0, 1);
                        if (processedDefinition[0] != ' ') { return false; } // Only ' ' allowed after wild-card
                    }
                }
                else if (processedDefinition[0] == negationCharacter)
                {
                    processedDefinition = processedDefinition.Remove(0, 1);
                    definitionItem += negationCharacter.ToString();
                    if (processedDefinition.Length > 1)  // Otherwise: done
                    {                        
                        if (processedDefinition[0] == optionsStartCharacter)
                        {
                            int nextIndex = processedDefinition.IndexOf(optionsEndCharacter);
                            if (nextIndex < 0) { return false; } // Unbalanced "["
                            else
                            {
                                definitionItem += processedDefinition.Substring(0, nextIndex + 1);
                                processedDefinitionList.Add(definitionItem);
                                processedDefinition = processedDefinition.Remove(0, nextIndex + 1);
                                definitionItem = "";
                            }
                        }
                        else { return false; }
                    }
                }                
                else if (processedDefinition[0] == ' ')  // End of word: chop
                {
                    if (definitionItem != "")
                    {
                        processedDefinitionList.Add(definitionItem);
                        definitionItem = "";
                    }
                }
                else
                {
                    definitionItem += processedDefinition[0];
                }
                if (processedDefinition.Length > 0)
                {
                    processedDefinition = processedDefinition.Remove(0, 1);
                }
            }
            if (definitionItem != "") { processedDefinitionList.Add(definitionItem); }


            // Remove all punctuation (should not be there in the first place, but...)
            for (int ii = 0; ii < processedDefinitionList.Count; ii++)
            {
                string reducedString = processedDefinitionList[ii].TrimEnd(new char[] { ',', ':', '.', ';', '!', '?' });
                processedDefinitionList[ii] = reducedString;
            }

            // Next, generate all possible options, based on the definition string, and
            // store the options in the patternItemList
            patternItemList = new List<PatternItem>();
            for (int ii = 0; ii < processedDefinitionList.Count; ii++)
            {
                PatternItem patternItem = new PatternItem();
                List<string> optionList = new List<string>();
                string processedDefinitionItem = processedDefinitionList[ii];
                if (processedDefinitionItem[0] == wildCardCharacter)
                {
                    patternItem.PositiveList.Add(wildCardCharacter.ToString());
                    if (processedDefinitionItem.Length == 1)  // just the wildcard
                    {                      
                        patternItemList.Add(patternItem); // Wild-card in positive list, and empty negative list: Anything matches
                    }
                    else
                    {
                        return false;  // Nothing allowed after "*"
                    }
                }
                else if (processedDefinitionItem[0] == negationCharacter)
                {
                    processedDefinitionItem = processedDefinitionItem.Remove(0, 1);
                    if (processedDefinitionItem.Length > 0)
                    {
                        if ((processedDefinitionItem[0] == optionsStartCharacter) &&
                            (processedDefinitionItem[processedDefinitionItem.Length - 1] == optionsEndCharacter))  // Should always be the case, but...
                        {
                            List<string> processedDefinitionItemSplit = processedDefinitionItem.ToLower().Split(new char[] { '[', ' ', ']' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            foreach (string item in processedDefinitionItemSplit)
                            {
                                patternItem.NegativeList.Add(item);
                            }
                            patternItemList.Add(patternItem);
                        }
                        else { return false; }  // Unmatched [ ]
                    }
                }
                else if (processedDefinitionItem[0] == optionsStartCharacter)
                {
                    if ((processedDefinitionItem.StartsWith(optionsStartCharacter.ToString()) &&
                        (processedDefinitionItem.EndsWith(optionsEndCharacter.ToString()))))
                    {
                        List<string> processedDefinitionItemSplit = processedDefinitionItem.ToLower().Split(new char[] { '[', ' ', ']' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string item in processedDefinitionItemSplit)
                        {
                            patternItem.PositiveList.Add(item);
                        }
                        patternItemList.Add(patternItem);
                    }
                    else { return false; }
                }
                else  // Either just a word, or a query term ( <...> )
                {
                    if (processedDefinitionItem[0] == queryStartCharacter)
                    {
                        patternItem.PositiveList.Add(processedDefinitionItem);
                    }
                    else
                    {
                        patternItem.PositiveList.Add(processedDefinitionItem.ToLower());
                    }
                    patternItemList.Add(patternItem);
                }
            }

            // For query terms, only one item is allowed in the positive list. Check:


            return true;
        }

        public Boolean IsMatching(string checkString)
        {
            queryTerms = new List<string>();
            if (patternItemList == null) { ProcessDefinition();  }
            List<string> checkStringSplit = checkString.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            checkStringSplit[checkStringSplit.Count - 1] = checkStringSplit.Last().TrimEnd(new char[] { '?', '.', '!' });
            int checkIndex = 0;
            while (checkIndex < checkStringSplit.Count)
            {
                string checkStringItem = checkStringSplit[checkIndex].ToLower();
                if (checkIndex >= patternItemList.Count) { return false; }
                else
                {
                    PatternItem patternItem = patternItemList[checkIndex];
                    Boolean wordMatchFound = false;
                    if ((patternItem.PositiveList.Count == 1) && (patternItem.NegativeList.Count == 0))  // For query terms, only one option is possible
                    {
                        string option = patternItem.PositiveList[0];
                        if ((option[0] == queryStartCharacter) &&
                            (option[option.Length - 1] == queryEndCharacter))
                        {
                            wordMatchFound = true;
                            queryTerms.Add(checkStringItem);
                        }
                        else if (option == "*") // wild-card: Anything (non-empty) matches
                        {
                            wordMatchFound = true;
                            if (checkIndex == (patternItemList.Count - 1)) // If the pattern ENDS with a wild-card, then return a positive match
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (option == checkStringItem)
                            {
                                wordMatchFound = true;
                            }
                        }
                    }
                    else if (patternItem.NegativeList.Count == 0) // Check for positive match only
                    {
                        foreach (string option in patternItem.PositiveList)
                        {
                            if (option == checkStringItem)
                            {
                                wordMatchFound = true;
                            }
                        }
                    }
                    else // Non-empty negative list
                    {
                        wordMatchFound = true;
                        foreach (string option in patternItem.NegativeList)
                        {
                            if (option == checkStringItem)
                            {
                                wordMatchFound = false;
                                break;  // => wordMatchFound = false (in this case, because a word in the NegativeList DOES match.
                            }
                        }
                    }
                    if (!wordMatchFound) { return false; }
                }
                checkIndex++;
            }
            // Return false if the entered sentence is too short (relative to the pattern), UNLESS the
            // pattern ENDS with a wild card (see above).
            if (checkIndex != patternItemList.Count) { return false; }
            else { return true; }
        }

        public List<string> GetQueryTerms()
        {
            return queryTerms;
        }

        public string GetString(Random randomNumberGenerator, List<StringMemoryItem> queryMemoryItemList)
        {
            string outputString = "";
            List<string> queryItems = new List<string>();
            if (queryMemoryItemList != null)
            {
                foreach (StringMemoryItem queryMemoryItem in queryMemoryItemList)
                {
                    string queryMemoryItemString = (string)queryMemoryItem.GetContent();
                    queryItems.Add(queryMemoryItemString);
                }
            }

            foreach (PatternItem patternItem in patternItemList)  // Only the PositiveList is used when generating output
            {
                if (patternItem.PositiveList.Count > 0) // Should ALWAYS be the case, but just to be sure..
                {
                    if (patternItem.PositiveList[0][0] == queryStartCharacter)
                    {
                        string queryTagString = patternItem.PositiveList[0];
                        MemoryItem queryMemoryItem = queryMemoryItemList.Find(q => q.TagList.Contains(queryTagString));
                        outputString += queryMemoryItem.GetContent() + " ";
                    }
                    else
                    {
                        int numberOfOptions = patternItem.PositiveList.Count;
                        int selectedIndex = randomNumberGenerator.Next(0, numberOfOptions); // Exclusive upper bound
                        outputString += patternItem.PositiveList[selectedIndex] + " ";
                    }
                }
            }
            outputString = outputString.TrimEnd(new char[] { ' ' });
            return outputString;
        }

        public string GetVerbatimString()
        {
            return definition; // verbatimOutputString;
        }

        [DataMember]
        public string Definition
        {
            get { return definition; }
            set { definition = value; }
        }
    }
}
