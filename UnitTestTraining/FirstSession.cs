namespace UnitTestTraining
{
    public class FirstSession
    {
        //Assert equals
        public int AddTwoNumbers(int i, int j)
        {
            return i + j;
        }

        //Assert empty, contains, single
        public List<string> FindNameBeginningWith(string prefix)
        {
            var nameList = new List<string>
            {
                "Henry",
                "Martin",
                "Mick"
            };

            return nameList.Where(x => x.StartsWith(prefix)).ToList();
        }

        //Assert null, not null
        public string FindTopNameEndWith(string postfix)
        {
            var nameList = new List<string>
            {
                "Henry",
                "Martin",
                "Mick"
            };

            return nameList.FirstOrDefault(x => x.EndsWith(postfix));
        }

        //Assert true, false
        public bool AreStringsEqualLength(string stringOne, string stringTwo)
        {
            return stringOne.Length == stringTwo.Length;
        }

        //Assert throws
        public void ProcessWord(string word)
        {
            var bannedWords = new List<string>
            {
                "Hello",
                "World"
            };

            if (bannedWords.Contains(word))
            {
                throw new NullReferenceException("Word not allowed");
            }
        }
    }
}