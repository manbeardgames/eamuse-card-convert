/*
 * Converted from python to C# based on source provided by
 * DragonMinded
 * https://github.com/DragonMinded/bemaniutils/blob/master/bemani/common/card.py
 */

using System;

namespace EamuseCardConvert
{
    /// <summary>
    ///     Algorithm for converting between the Card ID as stored in an
    ///     eAmusement card and the 16 character card string as shown on
    ///     the back of a card and in-game.
    /// </summary>
    public static class CardCipher
    {
        private static readonly uint[] _key = new uint[]
        {
            0x20d0d03c, 0x868ecb41, 0xbcd89c84, 0x4c0e0d0d,
            0x84fc30ac, 0x4cc1890e, 0xfc5418a4, 0x02c50f44,
            0x68acb4e0, 0x06cd4a4e, 0xcc28906c, 0x4f0c8ac0,
            0xb03ca468, 0x884ac7c4, 0x389490d8, 0xcf80c6c2,
            0x58d87404, 0xc48ec444, 0xb4e83c50, 0x498d0147,
            0x64f454c0, 0x4c4701c8, 0xec302cc4, 0xc6c949c1,
            0xc84c00f0, 0xcdcc49cc, 0x883c5cf4, 0x8b0fcb80,
            0x703cc0b0, 0xcb820a8d, 0x78804c8c, 0x4fca830e,
            0x80d0f03c, 0x8ec84f8c, 0x98c89c4c, 0xc80d878f,
            0x54bc949c, 0xc801c5ce, 0x749078dc, 0xc3c80d46,
            0x2c8070f0, 0x0cce4dcf, 0x8c3874e4, 0x8d448ac3,
            0x987cac70, 0xc0c20ac5, 0x288cfc78, 0xc28543c8,
            0x4c8c7434, 0xc50e4f8d, 0x8468f4b4, 0xcb4a0307,
            0x2854dc98, 0x48430b45, 0x6858fce8, 0x4681cd49,
            0xd04808ec, 0x458d0fcb, 0xe0a48ce4, 0x880f8fce,
            0x7434b8fc, 0xce080a8e, 0x5860fc6c, 0x46c886cc,
            0xd01098a4, 0xce090b8c, 0x1044cc2c, 0x86898e0f,
            0xd0809c3c, 0x4a05860f, 0x54b4f80c, 0x4008870e,
            0x1480b88c, 0x0ac8854f, 0x1c9034cc, 0x08444c4e,
            0x0cb83c64, 0x41c08cc6, 0x1c083460, 0xc0c603ce,
            0x2ca0645c, 0x818246cb, 0x0408e454, 0xc5464487,
            0x88607c18, 0xc1424187, 0x284c7c90, 0xc1030509,
            0x40486c94, 0x4603494b, 0xe0404ce4, 0x4109094d,
            0x60443ce4, 0x4c0b8b8d, 0xe054e8bc, 0x02008e89
        };

        private static readonly uint[] _lut_a0 = new uint[]
        {
            0x02080008, 0x02082000, 0x00002008, 0x00000000,
            0x02002000, 0x00080008, 0x02080000, 0x02082008,
            0x00000008, 0x02000000, 0x00082000, 0x00002008,
            0x00082008, 0x02002008, 0x02000008, 0x02080000,
            0x00002000, 0x00082008, 0x00080008, 0x02002000,
            0x02082008, 0x02000008, 0x00000000, 0x00082000,
            0x02000000, 0x00080000, 0x02002008, 0x02080008,
            0x00080000, 0x00002000, 0x02082000, 0x00000008,
            0x00080000, 0x00002000, 0x02000008, 0x02082008,
            0x00002008, 0x02000000, 0x00000000, 0x00082000,
            0x02080008, 0x02002008, 0x02002000, 0x00080008,
            0x02082000, 0x00000008, 0x00080008, 0x02002000,
            0x02082008, 0x00080000, 0x02080000, 0x02000008,
            0x00082000, 0x00002008, 0x02002008, 0x02080000,
            0x00000008, 0x02082000, 0x00082008, 0x00000000,
            0x02000000, 0x02080008, 0x00002000, 0x00082008
        };

        private static readonly uint[] _lut_a1 = new uint[] {
            0x08000004, 0x00020004, 0x00000000, 0x08020200,
            0x00020004, 0x00000200, 0x08000204, 0x00020000,
            0x00000204, 0x08020204, 0x00020200, 0x08000000,
            0x08000200, 0x08000004, 0x08020000, 0x00020204,
            0x00020000, 0x08000204, 0x08020004, 0x00000000,
            0x00000200, 0x00000004, 0x08020200, 0x08020004,
            0x08020204, 0x08020000, 0x08000000, 0x00000204,
            0x00000004, 0x00020200, 0x00020204, 0x08000200,
            0x00000204, 0x08000000, 0x08000200, 0x00020204,
            0x08020200, 0x00020004, 0x00000000, 0x08000200,
            0x08000000, 0x00000200, 0x08020004, 0x00020000,
            0x00020004, 0x08020204, 0x00020200, 0x00000004,
            0x08020204, 0x00020200, 0x00020000, 0x08000204,
            0x08000004, 0x08020000, 0x00020204, 0x00000000,
            0x00000200, 0x08000004, 0x08000204, 0x08020200,
            0x08020000, 0x00000204, 0x00000004, 0x08020004
        };

        private static readonly uint[] _lut_a2 = new uint[] {
            0x80040100, 0x01000100, 0x80000000, 0x81040100,
            0x00000000, 0x01040000, 0x81000100, 0x80040000,
            0x01040100, 0x81000000, 0x01000000, 0x80000100,
            0x81000000, 0x80040100, 0x00040000, 0x01000000,
            0x81040000, 0x00040100, 0x00000100, 0x80000000,
            0x00040100, 0x81000100, 0x01040000, 0x00000100,
            0x80000100, 0x00000000, 0x80040000, 0x01040100,
            0x01000100, 0x81040000, 0x81040100, 0x00040000,
            0x81040000, 0x80000100, 0x00040000, 0x81000000,
            0x00040100, 0x01000100, 0x80000000, 0x01040000,
            0x81000100, 0x00000000, 0x00000100, 0x80040000,
            0x00000000, 0x81040000, 0x01040100, 0x00000100,
            0x01000000, 0x81040100, 0x80040100, 0x00040000,
            0x81040100, 0x80000000, 0x01000100, 0x80040100,
            0x80040000, 0x00040100, 0x01040000, 0x81000100,
            0x80000100, 0x01000000, 0x81000000, 0x01040100
        };

        private static readonly uint[] _lut_a3 = new uint[] {
            0x04010801, 0x00000000, 0x00010800, 0x04010000,
            0x04000001, 0x00000801, 0x04000800, 0x00010800,
            0x00000800, 0x04010001, 0x00000001, 0x04000800,
            0x00010001, 0x04010800, 0x04010000, 0x00000001,
            0x00010000, 0x04000801, 0x04010001, 0x00000800,
            0x00010801, 0x04000000, 0x00000000, 0x00010001,
            0x04000801, 0x00010801, 0x04010800, 0x04000001,
            0x04000000, 0x00010000, 0x00000801, 0x04010801,
            0x00010001, 0x04010800, 0x04000800, 0x00010801,
            0x04010801, 0x00010001, 0x04000001, 0x00000000,
            0x04000000, 0x00000801, 0x00010000, 0x04010001,
            0x00000800, 0x04000000, 0x00010801, 0x04000801,
            0x04010800, 0x00000800, 0x00000000, 0x04000001,
            0x00000001, 0x04010801, 0x00010800, 0x04010000,
            0x04010001, 0x00010000, 0x00000801, 0x04000800,
            0x04000801, 0x00000001, 0x04010000, 0x00010800
        };

        private static readonly uint[] _lut_b0 = new uint[] {
            0x00000400, 0x00000020, 0x00100020, 0x40100000,
            0x40100420, 0x40000400, 0x00000420, 0x00000000,
            0x00100000, 0x40100020, 0x40000020, 0x00100400,
            0x40000000, 0x00100420, 0x00100400, 0x40000020,
            0x40100020, 0x00000400, 0x40000400, 0x40100420,
            0x00000000, 0x00100020, 0x40100000, 0x00000420,
            0x40100400, 0x40000420, 0x00100420, 0x40000000,
            0x40000420, 0x40100400, 0x00000020, 0x00100000,
            0x40000420, 0x00100400, 0x40100400, 0x40000020,
            0x00000400, 0x00000020, 0x00100000, 0x40100400,
            0x40100020, 0x40000420, 0x00000420, 0x00000000,
            0x00000020, 0x40100000, 0x40000000, 0x00100020,
            0x00000000, 0x40100020, 0x00100020, 0x00000420,
            0x40000020, 0x00000400, 0x40100420, 0x00100000,
            0x00100420, 0x40000000, 0x40000400, 0x40100420,
            0x40100000, 0x00100420, 0x00100400, 0x40000400
        };

        private static readonly uint[] _lut_b1 = new uint[] {
            0x00800000, 0x00001000, 0x00000040, 0x00801042,
            0x00801002, 0x00800040, 0x00001042, 0x00801000,
            0x00001000, 0x00000002, 0x00800002, 0x00001040,
            0x00800042, 0x00801002, 0x00801040, 0x00000000,
            0x00001040, 0x00800000, 0x00001002, 0x00000042,
            0x00800040, 0x00001042, 0x00000000, 0x00800002,
            0x00000002, 0x00800042, 0x00801042, 0x00001002,
            0x00801000, 0x00000040, 0x00000042, 0x00801040,
            0x00801040, 0x00800042, 0x00001002, 0x00801000,
            0x00001000, 0x00000002, 0x00800002, 0x00800040,
            0x00800000, 0x00001040, 0x00801042, 0x00000000,
            0x00001042, 0x00800000, 0x00000040, 0x00001002,
            0x00800042, 0x00000040, 0x00000000, 0x00801042,
            0x00801002, 0x00801040, 0x00000042, 0x00001000,
            0x00001040, 0x00801002, 0x00800040, 0x00000042,
            0x00000002, 0x00001042, 0x00801000, 0x00800002
        };

        private static readonly uint[] _lut_b2 = new uint[] {
            0x10400000, 0x00404010, 0x00000010, 0x10400010,
            0x10004000, 0x00400000, 0x10400010, 0x00004010,
            0x00400010, 0x00004000, 0x00404000, 0x10000000,
            0x10404010, 0x10000010, 0x10000000, 0x10404000,
            0x00000000, 0x10004000, 0x00404010, 0x00000010,
            0x10000010, 0x10404010, 0x00004000, 0x10400000,
            0x10404000, 0x00400010, 0x10004010, 0x00404000,
            0x00004010, 0x00000000, 0x00400000, 0x10004010,
            0x00404010, 0x00000010, 0x10000000, 0x00004000,
            0x10000010, 0x10004000, 0x00404000, 0x10400010,
            0x00000000, 0x00404010, 0x00004010, 0x10404000,
            0x10004000, 0x00400000, 0x10404010, 0x10000000,
            0x10004010, 0x10400000, 0x00400000, 0x10404010,
            0x00004000, 0x00400010, 0x10400010, 0x00004010,
            0x00400010, 0x00000000, 0x10404000, 0x10000010,
            0x10400000, 0x10004010, 0x00000010, 0x00404000
        };

        private static readonly uint[] _lut_b3 = new uint[] {
            0x00208080, 0x00008000, 0x20200000, 0x20208080,
            0x00200000, 0x20008080, 0x20008000, 0x20200000,
            0x20008080, 0x00208080, 0x00208000, 0x20000080,
            0x20200080, 0x00200000, 0x00000000, 0x20008000,
            0x00008000, 0x20000000, 0x00200080, 0x00008080,
            0x20208080, 0x00208000, 0x20000080, 0x00200080,
            0x20000000, 0x00000080, 0x00008080, 0x20208000,
            0x00000080, 0x20200080, 0x20208000, 0x00000000,
            0x00000000, 0x20208080, 0x00200080, 0x20008000,
            0x00208080, 0x00008000, 0x20000080, 0x00200080,
            0x20208000, 0x00000080, 0x00008080, 0x20200000,
            0x20008080, 0x20000000, 0x20200000, 0x00208000,
            0x20208080, 0x00008080, 0x00208000, 0x20200080,
            0x00200000, 0x20000080, 0x20008000, 0x00000000,
            0x00008000, 0x00200000, 0x20200080, 0x00208080,
            0x20000000, 0x20208000, 0x00000080, 0x20008080
        };

        private static readonly string _valid_chars = "0123456789ABCDEFGHJKLMNPRSTUWXYZ";

        /// <summary>
        ///     Given a card id, returns a value indicating the type
        ///     of eamusement card. (1 = old-style, 2 = FeliCa)
        /// </summary>
        /// <param name="cardID">
        ///     16 digit card ID
        /// </param>
        /// <returns>
        ///     An integer value of 1 is returned if the card ID is of an old-style
        ///     eAmusement card, 2 if the card ID is of the newer FeliCa style 
        ///     eAmusement card; otherwise an exception is thrown.
        /// </returns>
        /// <exception cref="CardTypeException">
        ///     Thrown if the card type cannot be deteremined.
        /// </exception>
        public static int TypeFromCardID(string cardID)
        {
            //  We can determine the card type based on the first two values of the
            //  card ID.
            int cardType = cardID[..2].ToUpper() switch
            {
                "E0" => 1,
                "01" => 2,
                _ => throw new Exception("Unknown card type")
            };

            return cardType;
        }

        /// <summary>
        ///     Given a card ID as stored on a card (Usually starting with E004), convert it to the
        ///     card string as shown on the back of the card.
        /// </summary>
        /// <param name="cardID">
        ///     16 digit card ID (hex values stored as string)
        /// </param>
        /// <returns>
        ///     String representation of the card string printed on the back of the
        ///     eAmusement card.
        /// </returns>
        public static string Encode(string cardID)
        {
            //  The card ID must be exactly 16 characters to continue.
            if (cardID.Length != 16)
            {
                throw new Exception($"Expected 16-character card length, got {cardID.Length}");
            }

            byte[] cardint = new byte[8];
            int charIndex = 0;
            for (int i = 0; i < 8; i++)
            {
                cardint[i] = (byte)Convert.ToInt32(cardID[(charIndex)..(charIndex + 2)], 16);
                charIndex += 2;
            }

            //  Reverse bytes
            byte[] reverse = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                reverse[7 - i] = cardint[i];
            }

            //  Encipher
            var ciphered = Encode(reverse);

            //  Convert 8x8 bit bytes into 13 x 5 bit groups (sort of)
            int[] bits = new int[65];
            for (int i = 0; i < 64; i++)
            {
                bits[i] = (ciphered[i >> 3] >> (~i & 7)) & 1;
            }

            int[] groups = new int[16];
            for (int i = 0; i < 13; i++)
            {
                groups[i] = (bits[i * 5 + 0] << 4) |
                            (bits[i * 5 + 1] << 3) |
                            (bits[i * 5 + 2] << 2) |
                            (bits[i * 5 + 3] << 1) |
                            (bits[i * 5 + 4] << 0);
            }

            //  Smear 13 groups out into 14 groups
            groups[13] = 1;
            groups[0] ^= TypeFromCardID(cardID);

            for (int i = 0; i < 14; i++)
            {
                int index = i - 1;
                index = index < 0 ? groups.Length - 1 : index;
                groups[i] ^= groups[index];
            }

            //  Scheme field is 1 for old-style, 2 for felica cards
            groups[14] = TypeFromCardID(cardID);
            groups[15] = Checksum(groups);

            //  Convert to chars and return
            string result = string.Empty;
            for (int i = 0; i < groups.Length; i++)
            {
                result += _valid_chars[groups[i]];
            }

            return result;
        }

        /// <summary>
        ///     Given a card string as shown on the back of the card, return the card ID
        ///     as stored on the card itself.  Does some sanitization to remove dashes,
        ///     spaces, and convert confusing characters (1, L and 0, O) before converting.
        /// </summary>
        /// <param name="cardID">
        ///     String representation of the card string printed on the back of the
        ///     eAmusement card.
        /// </param>
        /// <returns>
        ///     16 digit card ID (hex values stored as string)
        /// </returns>
        public static string Decode(string cardID)
        {
            //  Ensure all characters are uppercase so we can santaize properly.
            cardID.ToUpper();

            //  Remove all spaces from the card id
            cardID = cardID.Replace(" ", string.Empty);

            //  Remve all hyphens from the card id
            cardID = cardID.Replace("-", string.Empty);

            //  Convert all 'I' characters to '1'
            cardID = cardID.Replace('I', '1');

            //  Convert all 'O' characters to '0'
            cardID = cardID.Replace('O', '0');

            //  Card ID must be exactly 16-characters
            if (cardID.Length != 16)
            {
                throw new Exception($"Expected 16-character card ID length, got {cardID.Length}");
            }

            //  Ensure Card ID only contains valid characters
            for (int i = 0; i < cardID.Length; i++)
            {
                if (!_valid_chars.Contains(cardID[i]))
                {
                    throw new Exception($"Got unexpected character {cardID[i]} in card ID");
                }
            }

            //  Convert chars to groups
            int[] groups = new int[16];
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (cardID[i] == _valid_chars[j])
                    {
                        groups[i] = j;
                        break;
                    }
                }
            }

            //  Verify that the card type is 1 (old-style) or 2 (new FeliCa)
            if (groups[14] != 1 && groups[14] != 2)
            {
                throw new Exception($"Unrecognized card type. Expected card type of 1 or 2, got {groups[14]}.");
            }

            //  Verify the checksum
            if (groups[15] != Checksum(groups))
            {
                throw new Exception("Bad card number");
            }


            //  Un-smear 14 fields back into 13
            for (int i = 13; i > 0; i--)
            {
                groups[i] ^= groups[i - 1];
            }
            groups[0] ^= groups[14];

            //  Explode groups into bits
            int[] bits = new int[64];

            for (int i = 0; i < 64; i++)
            {
                bits[i] = (groups[(i / 5)] >> (4 - (i % 5))) & 1;
            }

            //  Re-pack bits into eight bytes
            byte[] ciphered = new byte[8];

            for (int i = 0; i < 64; i++)
            {
                ciphered[(i / 8)] |= Convert.ToByte(bits[i] << (~i & 7));
            }

            //  Decipher and reverse
            byte[] deciphered = Decode(ciphered);
            byte[] reverse = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                reverse[i] = deciphered[7 - i];
            }

            //  Build the card id by concating the hex value of each byte
            //  as a string.
            string finalValue = string.Empty;
            for (int i = 0; i < reverse.Length; i++)
            {
                finalValue += reverse[i].ToString("X2");
            }

            //  Ensure that the now decoded value has the same matching card type
            //  as what we had beore.
            int cardType = TypeFromCardID(finalValue);
            if (groups[14] != cardType)
            {
                throw new Exception("Mismatched card type after decoding.");
            }

            return finalValue;
        }

        private static int Checksum(int[] data)
        {
            int checksum = 0;

            for (int i = 0; i < 15; i++)
            {
                checksum += (i % 3 + 1) * data[i];
            }

            while (checksum >= 0x20)
            {
                checksum = (checksum & 0x1F) + (checksum >> 5);
            }

            return checksum;
        }

        private static byte[] Encode(byte[] input)
        {
            if (input.Length != 8)
            {
                throw new Exception($"Expected 8-byte input, got {input.Length}");
            }

            ////  Make a copy of the input data and work with the copy to ensure
            ////  no values are changed from the input byte array.
            //byte[] input = new byte[inBytes.Length];
            //Buffer.BlockCopy(inBytes, 0, input, 0, inBytes.Length);

            byte[] output = new byte[8];

            FromInt(output, OperatorA(0x00, ToInt(input)));
            FromInt(output, OperatorB(0x20, ToInt(output)));
            FromInt(output, OperatorA(0x40, ToInt(output)));

            return output;


        }

        private static byte[] Decode(byte[] input)
        {
            if (input.Length != 8)
            {
                throw new Exception($"Expected 8-byte input, got {input.Length}");
            }

            //byte[] inp = new byte[input.Length];
            //Buffer.BlockCopy(input, 0, inp, 0, input.Length);

            byte[] output = new byte[8];

            FromInt(output, OperatorB(0x40, ToInt(input)));
            FromInt(output, OperatorA(0x20, ToInt(output)));
            FromInt(output, OperatorB(0x00, ToInt(output)));

            return output;
        }

        private static long ToInt(byte[] data)
        {
            var inX = (data[0] & 0xFF) |
                      ((data[1] & 0xFF) << 8) |
                      ((data[2] & 0xFF) << 16) |
                      ((data[3] & 0xFF) << 24);

            var inY = (data[4] & 0xFF) |
                      ((data[5] & 0xFF) << 8) |
                      ((data[6] & 0xFF) << 16) |
                      ((data[7] & 0xFF) << 24);

            var v7 = ((((inX ^ (inY >> 4)) & 0xF0F0F0F) << 4) ^ inY) & 0xFFFFFFFF;
            var v8 = (((inX ^ (inY >> 4)) & 0xF0F0F0F) ^ inX) & 0xFFFFFFFF;

            var v9 = ((v7 ^ (v8 >> 16))) & 0x0000FFFF;
            var v10 = (((v7 ^ (v8 >> 16)) << 16) ^ v8) & 0xFFFFFFFF;

            var v11 = (v9 ^ v7) & 0xFFFFFFFF;
            var v12 = (v10 ^ (v11 >> 2)) & 0x33333333;
            var v13 = (v11 ^ (v12 << 2)) & 0xFFFFFFFF;

            var v14 = (v12 ^ v10) & 0xFFFFFFFF;
            var v15 = (v13 ^ (v14 >> 8)) & 0x00FF00FF;
            var v16 = (v14 ^ (v15 << 8)) & 0xFFFFFFFF;

            var v17 = ROR(v15 ^ v13, 1);
            var v18 = (v16 ^ v17) & 0x55555555;

            var v3 = ROR(v18 ^ v16, 1);
            var v4 = (v18 ^ v17) & 0xFFFFFFFF;

            var result = ((v3 & 0xFFFFFFFF) << 32) | (v4 & 0xFFFFFFFF);
            return result;
        }

        private static void FromInt(byte[] data, long state)
        {
            long v3 = (state >> 32) & 0xFFFFFFFF;
            long v4 = state & 0xFFFFFFFF;

            long v22 = ROR(v4, 31);
            long v23 = (v3 ^ v22) & 0x55555555;
            long v24 = (v23 ^ v22) & 0xFFFFFFFF;

            long v25 = ROR(v23 ^ v3, 31);
            long v26 = (v25 ^ (v24 >> 8)) & 0x00FF00FF;
            long v27 = (v24 ^ (v26 << 8)) & 0xFFFFFFFF;

            long v28 = (v26 ^ v25) & 0xFFFFFFFF;
            long v29 = ((v28 >> 2) ^ v27) & 0x33333333;
            long v30 = ((v29 << 2) ^ v28) & 0xFFFFFFFF;

            long v31 = (v29 ^ v27) & 0xFFFFFFFF;
            long v32 = (v30 ^ (v31 >> 16)) & 0x0000FFFF;
            long v33 = (v31 ^ (v32 << 16)) & 0xFFFFFFFF;

            long v34 = (v32 ^ v30) & 0xFFFFFFFF;
            long v35 = (v33 ^ (v34 >> 4)) & 0xF0F0F0F;

            long outY = ((v35 << 4) ^ v34) & 0xFFFFFFFF;
            long outX = (v35 ^ v33) & 0xFFFFFFFF;

            data[0] = Convert.ToByte(outX & 0xFF);
            data[1] = Convert.ToByte((outX >> 8) & 0xFF);
            data[2] = Convert.ToByte((outX >> 16) & 0xFF);
            data[3] = Convert.ToByte((outX >> 24) & 0xFF);
            data[4] = Convert.ToByte(outY & 0xFF);
            data[5] = Convert.ToByte((outY >> 8) & 0xFF);
            data[6] = Convert.ToByte((outY >> 16) & 0xFF);
            data[7] = Convert.ToByte((outY >> 24) & 0xFF);
        }

        private static long OperatorA(int off, long state)
        {
            var v3 = (state >> 32) & 0xFFFFFFFF;
            var v4 = state & 0xFFFFFFFF;

            for (int i = 0; i < 32; i += 4)
            {
                var v20 = ROR(v3 ^ _key[off + i + 1], 28);

                v4 ^= _lut_b0[(v20 >> 26) & 0x3F] ^
                      _lut_b1[(v20 >> 18) & 0x3F] ^
                      _lut_b2[(v20 >> 10) & 0x3F] ^
                      _lut_b3[(v20 >> 2) & 0x3F] ^
                      _lut_a0[((v3 ^ _key[off + i]) >> 26) & 0x3F] ^
                      _lut_a1[((v3 ^ _key[off + i]) >> 18) & 0x3F] ^
                      _lut_a2[((v3 ^ _key[off + i]) >> 10) & 0x3F] ^
                      _lut_a3[((v3 ^ _key[off + i]) >> 2) & 0x3F];

                var v21 = ROR(v4 ^ _key[off + i + 3], 28);

                v3 ^= _lut_b0[(v21 >> 26) & 0x3F] ^
                      _lut_b1[(v21 >> 18) & 0x3F] ^
                      _lut_b2[(v21 >> 10) & 0x3F] ^
                      _lut_b3[(v21 >> 2) & 0x3F] ^
                      _lut_a0[((v4 ^ _key[off + i + 2]) >> 26) & 0x3F] ^
                      _lut_a1[((v4 ^ _key[off + i + 2]) >> 18) & 0x3F] ^
                      _lut_a2[((v4 ^ _key[off + i + 2]) >> 10) & 0x3F] ^
                      _lut_a3[((v4 ^ _key[off + i + 2]) >> 2) & 0x3F];
            }

            var result = ((v3 & 0xFFFFFFFF) << 32) | (v4 & 0xFFFFFFFF);
            return result;
        }

        private static long OperatorB(int off, long state)
        {
            var v3 = (state >> 32) & 0xFFFFFFFF;
            var v4 = state & 0xFFFFFFFF;

            for (int i = 0; i < 32; i += 4)
            {
                var v20 = ROR(v3 ^ _key[off + 31 - i], 28);

                v4 ^= _lut_a0[((v3 ^ _key[off + 30 - i]) >> 26) & 0x3F] ^
                      _lut_a1[((v3 ^ _key[off + 30 - i]) >> 18) & 0x3F] ^
                      _lut_a2[((v3 ^ _key[off + 30 - i]) >> 10) & 0x3F] ^
                      _lut_a3[((v3 ^ _key[off + 30 - i]) >> 2) & 0x3F] ^
                      _lut_b0[(v20 >> 26) & 0x3F] ^
                      _lut_b1[(v20 >> 18) & 0x3F] ^
                      _lut_b2[(v20 >> 10) & 0x3F] ^
                      _lut_b3[(v20 >> 2) & 0x3F];

                var v21 = ROR(v4 ^ _key[off + 29 - i], 28);

                v3 ^= _lut_a0[((v4 ^ _key[off + 28 - i]) >> 26) & 0x3F] ^
                      _lut_a1[((v4 ^ _key[off + 28 - i]) >> 18) & 0x3F] ^
                      _lut_a2[((v4 ^ _key[off + 28 - i]) >> 10) & 0x3F] ^
                      _lut_a3[((v4 ^ _key[off + 28 - i]) >> 2) & 0x3F] ^
                      _lut_b0[(v21 >> 26) & 0x3F] ^
                      _lut_b1[(v21 >> 18) & 0x3F] ^
                      _lut_b2[(v21 >> 10) & 0x3F] ^
                      _lut_b3[(v21 >> 2) & 0x3F];
            }

            return ((v3 & 0xFFFFFFFF) << 32) | (v4 & 0xFFFFFFFF);
        }

        private static long ROR(long val, int amount)
        {
            return ((val << (32 - amount)) & 0xFFFFFFFF) | ((val >> amount) & 0xFFFFFFFF);
        }
    }
}
