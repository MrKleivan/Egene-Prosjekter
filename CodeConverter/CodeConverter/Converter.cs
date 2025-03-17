using System.Text;

namespace CodeConverter;

public class Converter
{
    public string BinaryToHex(string binary)
    {
        return Convert.ToInt32(binary, 2).ToString("X");
    }

    public string BinaryToAscii(string binary)
    {
        binary = binary.PadLeft((binary.Length + 7) / 8 * 8, '0');
        StringBuilder ascii = new StringBuilder();
        for (int i = 0; i < binary.Length; i += 8)
        {
            string byteStr = binary.Substring(i, 8);
            ascii.Append((char)Convert.ToInt32(byteStr, 2));
        }
        return ascii.ToString();
    }
    
    public string HexToBinary(string hex)
    {
        return Convert.ToString(Convert.ToInt32(hex, 16), 2).PadLeft(hex.Length * 4, '0');
    }

    public string HexToAscii(string hex)
    {
        hex = hex.Replace(" ", "");
        StringBuilder ascii = new StringBuilder();
        for (int i = 0; i < hex.Length; i += 2)
        {
            string hexPair = hex.Substring(i, 2);
            ascii.Append((char)Convert.ToInt32(hexPair, 16));
        }
        return ascii.ToString();
    }

    public string AsciiToBinary(string ascii)
    {
        StringBuilder binary = new StringBuilder();
        foreach (var c in ascii)
        {
            binary.Append(Convert.ToString(c, 2).PadLeft(8, '0')+ " ");
        }
        return binary.ToString().Trim();
    }

    public string AsciiToHex(string ascii)
    {
        StringBuilder hex = new StringBuilder();
        foreach (var c in ascii)
        {
            hex.Append(((int)c).ToString("X2")+ " ");
        }
        return hex.ToString().Trim();
    }
    public string Run(string input, string output, string text)
    {
        if (input == "hex" && output == "ascii")
        {
            return HexToAscii(text);
        }

        if (input == "ascii" && output == "hex")
        {
            return AsciiToHex(text);
        }

        if (input == "binary" && output == "ascii")
        {
            return BinaryToAscii(text);
        }

        if (input == "binary" && output == "hex")
        {
            return BinaryToHex(text);
        }

        if (input == "hex" && output == "binary")
        {
            return HexToBinary(text);
        }

        if (input == "ascii" && output == "binary")
        {
            return AsciiToBinary(text);
        }

        return "Feil";
    }
}