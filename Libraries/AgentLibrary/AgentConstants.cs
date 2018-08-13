using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary
{
    public class AgentConstants
    {
        public static string DEFAULT_IP_ADDRESS = "127.0.0.1";
        public static int DEFAULT_COMMUNICATION_PORT = 7;

        public static string DEFAULT_NAME = "Agent1";
        public static string AGENT_PROCESS_NAME = "Agent";
        public static string SPEECH_PROCESS_NAME = "Speech";
        public static string LISTENER_PROCESS_NAME = "Listener";
        public static string FACE_PROCESS_NAME = "Face";
        public static string VISION_PROCESS_NAME = "Vision";
        public static string INTERNET_PROCESS_NAME = "Internet";

        public static string WORKING_MEMORY_NAME = "WorkingMemory";
        public static string LONG_TERM_MEMORY_NAME = "LongTermMemory";

        public static string DATE_TIME_FORMAT = "yyyyMMdd HH:mm:ss";

        public static char WILD_CARD_CHARACTER = '*';
        public static char NEGATION_CHARACTER = '!';
        public static char QUERY_START_CHARACTER = '<';
        public static char QUERY_END_CHARACTER = '>';
        public static char EXACT_EXPRESSION_START_CHARACTER = '{';
        public static char EXACT_EXPRESSION_END_CHARACTER = '}';
        public static char OPTIONS_START_CHARACTER = '[';
        public static char OPTIONS_END_CHARACTER = ']';

        public static string LISTENER_INPUT_TAG = "Source=Listener";
        public static string SPEECH_OUTPUT_TAG = "Target=Speech";
        public static string VISION_INPUT_TAG = "Source=Vision";
        public static string INTERNET_INPUT_TAG = "Source=Internet";
        public static string INTERNET_OUTPUT_TAG = "Target=Internet";
        public static string FACE_EXPRESSION_OUTPUT_TAG = "Target=Face";

        public static string QUERY_TAG_1 = "<Q1>";
        public static string QUERY_TAG_2 = "<Q2>";
        public static string QUERY_TAG_3 = "<Q3>";
        public static string QUERY_TAG_4 = "<Q4>";
        public static string QUERY_TAG_5 = "<Q5>";

        public static char MEMORY_ITEM_SEPARATION_CHARACTER = '|';
        public static char MEMORY_ITEM_ASSIGNMENT_CHARACTER = '=';
        public static char MEMORY_ITEM_LIST_LEFT_CHARACTER = '{';
        public static char MEMORY_ITEM_LIST_RIGHT_CHARACTER = '}';
        public static char MEMORY_ITEM_LIST_ITEM_SEPARATION_CHARACTER = ',';
        public static char INTERNET_INPUT_LEFT_SEPARATION_CHARACTER = '[';
        public static char INTERNET_INPUT_RIGHT_SEPARATION_CHARACTER = ']';
        public static char INTERNET_INPUT_TAG_LIST_LEFT_CHARACTER = '{';
        public static char INTERNET_INPUT_TAG_LIST_RIGHT_CHARACTER = '}';
        public static char INTERNET_INPUT_TAG_LIST_SEPARATOR_CHARACTER = ',';
        public static string SPEECH_OUTPUT_INTEGER = "#Integer";

        public static char INTERNET_SEARCH_REQUEST_SEPARATOR_CHARACTER = '|';
        public static char INTERNET_SEARCH_REQUEST_LIST_LEFT_CHARACTER = '{';
        public static char INTERNET_SEARCH_REQUEST_LIST_RIGHT_CHARACTER = '}';
        public static char INTERNET_SEARCH_REQUEST_LIST_SEPARATOR_CHARACTER = ',';

        public static string TIMEOUT_INTERVAL_PROPERTY_NAME = "TimeoutInterval";

        public static string DEFAULT_FAILURE_RESPONSE_1 = "I'm sorry, I don't understand";
        public static string DEFAULT_FAILURE_RESPONSE_2 = "Can you rephrase that, please?";

        public static string DEFAULT_TIME_START_STRING_1 = "It is ";
        public static string DEFAULT_TIME_START_STRING_2 = "The time is ";
    }
}
