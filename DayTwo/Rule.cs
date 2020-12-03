using System;

namespace DayTwo
{
    public readonly struct Rule
    {

        public Char Character { get; }

        public Int32 Minimum { get; }

        public Int32 Maximum { get; }

        public Rule(char character, Int32 min, Int32 max)
        {
            Character = character;
            Minimum = min;
            Maximum = max;
        }

        public bool IsValid(in String password)
        {
            Int32 count = 0;
            for (Int32 i = 0; i < password.Length; ++i)
                if (password[i] == this.Character)
                    count++;
            return count >= this.Minimum && count <= this.Maximum;
        }
        public bool IsValid2(in String password)
        {
            int min, max;
            min = Minimum - 1;
            max = Maximum - 1;

            //Immediate check
            if (min >= password.Length)
                return false;

            //First check if min entry there
            if (password.Length <= max)
                return password[min] == Character;

            //Now if we're here the range is on both spots
            if (password[min] == Character && password[max] != Character)
                return true;

            if (password[min] != Character && password[max] == Character)
                return true;








            //isValid |= password.Length > min && password.Length > max && password[min] == Character && password[max] != Character;
            //isValid |= password.Length > min && password.Length > max && password[min] != Character && password[max] == Character;
            //isValid |= !(password.Length > min && password.Length > max && password[min] == Character && password[max] == Character);
            //isValid |= password.Length > min && password.Length < max && password[min] == Character;



            return false;
        }


    }
}
