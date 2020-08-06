using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace PacketGenerator
{
    class Program
    {
        static string genPackets;       // 실시간으로 만들어지는 패킷 코드
        static ushort packetId;         // 1, 2, 3, ...
        static string packetEnums;
        static void Main(string[] args)
        {
            XmlReaderSettings settings = new XmlReaderSettings()        // 환경 설정
            {
                IgnoreComments = true,      // 주석 무시
                IgnoreWhitespace = true     // 스페이스 바 무시
            };

            using (XmlReader reader = XmlReader.Create("PDL.xml", settings))
            {
                reader.MoveToContent();      // 헤더를 건너뛰고 핵심 내용으로 바로 들어감

                while (reader.Read())        // string 방식으로 읽어드림
                {
                    if (reader.Depth == 1 && reader.NodeType == XmlNodeType.Element)    // Element 시작, EndElement 끝
                    {
                        ParsePacket(reader);
                    }
                    // System.Console.WriteLine(reader.Name + " " + reader["name"]);     // ["name"] : name이라는 콘텐츠만 읽는다.
                }
                string fileText = string.Format(PacketFormat.fileFormat, packetEnums, genPackets);
                File.WriteAllText("GenPacket.cs", fileText);
            }

            // reader.Dispose();        // 사용을 닫아줌 or using 사용(알아서 범위 내에서 Dispose 호출)
        }

        public static void ParsePacket(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.EndElement)  // 잘못 들어옴
            {
                return;
            }
            if (reader.Name.ToLower() != "packet")          // 소문자로 변환 후 packet이 아니면 return
            {
                System.Console.WriteLine("Invalid packet node");
                return;
            }

            string packetName = reader["name"];
            if (string.IsNullOrEmpty(packetName))       // 비어있는지
            {
                System.Console.WriteLine("Packet without name");
                return;
            }

            Tuple<string, string, string> tuple = ParseMembers(reader);
            genPackets += string.Format(PacketFormat.packetFormat, packetName, tuple.Item1, tuple.Item2, tuple.Item3);
            packetEnums += string.Format(PacketFormat.packetEnumFormat, packetName, ++packetId) + Environment.NewLine + "\t";
        }

        // {1} 멤버 변수들
        // {2} 멤버 변수 Read
        // {3} 멤버 변수 Write
        public static Tuple<string, string, string> ParseMembers(XmlReader reader)
        {
            string packetName = reader["name"];

            string memberCode = "";     // For Tuple
            string readCode = "";       // For Tuple
            string writeCode = "";      // For Tuple

            int depth = reader.Depth + 1;       // packet의 depth+1
            while (reader.Read())
            {
                if (reader.Depth != depth)
                {
                    break;
                }
                string memberName = reader["name"];
                if (string.IsNullOrEmpty(memberName))
                {
                    System.Console.WriteLine("Member without name");
                    return null;
                }
                if (string.IsNullOrEmpty(memberCode) == false)
                {
                    memberCode += Environment.NewLine;      // 내용이 있으면 개행
                }

                string memberType = reader.Name.ToLower();
                switch (memberType)
                {
                    case "bool":
                    case "byte":
                    case "short":
                    case "ushort":
                    case "int":
                    case "long":
                    case "float":
                    case "double":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readFormat, memberName, ToMemberType(memberType), memberType);
                        writeCode += string.Format(PacketFormat.writeFormat, memberName, memberType);
                        break;
                    case "string":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readStringFormat, memberName);
                        writeCode += string.Format(PacketFormat.writeStringFormat, memberName);
                        break;
                    case "list":
                        Tuple<string, string, string> tuple = ParseList(reader);
                        memberCode += tuple.Item1;
                        readCode += tuple.Item2;
                        writeCode += tuple.Item3;
                        break;
                    default:
                        break;
                }
            }
            // 정렬을 위해서
            memberCode = memberCode.Replace("\n", "\n\t");      // ("\n"을 \"n\t"으로 바꿈)
            readCode = readCode.Replace("\n", "\n\t\t");
            writeCode = writeCode.Replace("\n", "\n\t\t");

            // 한 번에 세 개를 반환
            return new Tuple<string, string, string>(memberCode, readCode, writeCode);
        }

        public static Tuple<string, string, string> ParseList(XmlReader reader)
        {
            string listName = reader["name"];
            if (string.IsNullOrEmpty(listName))
            {
                System.Console.WriteLine("List without name");
                return null;
            }

            Tuple<string, string, string> tuple = ParseMembers(reader);

            string memberCode = string.Format(PacketFormat.memberListFormat,
                FirstCharToUpper(listName),
                FirstCharToLower(listName),
                tuple.Item1,
                tuple.Item2,
                tuple.Item3
            );

            string readCode = string.Format(PacketFormat.readListFormat,
                FirstCharToUpper(listName),
                FirstCharToLower(listName)
            );

            string writeCode = string.Format(PacketFormat.writeListFormat,
                FirstCharToUpper(listName),
                FirstCharToLower(listName)
            );

            return new Tuple<string, string, string>(memberCode, readCode, writeCode);
        }

        public static string ToMemberType(string memberType)
        {
            switch (memberType)
            {
                case "bool":
                    return "ToBoolean";
                case "short":
                    return "ToInt16";
                case "ushort":
                    return "ToUINT16";
                case "int":
                    return "ToInt32";
                case "long":
                    return "ToInt64";
                case "float":
                    return "ToSingle";
                case "double":
                    return "ToDouble";
                default:
                    return "";
            }
        }

        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        public static string FirstCharToLower(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input[0].ToString().ToLower() + input.Substring(1);
        }
    }
}