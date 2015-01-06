﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEDExplorer
{
    class OsiPres
    {
        uint callingPresentationSelector;
        uint calledPresentationSelector;
        byte nextContextId;
        byte acseContextId;
        byte mmsContextId;
        int nextPayload_bufferIndex;
        int nextPayload_size;

        Iec61850State iecs;
        Logger logger;

        public OsiPres(Iec61850State iec)
        {
            iecs = iec;
            logger = iecs.logger;
        }

        public int Receive(Iec61850State iecs)
        {
            return 0;
        }

        public int Send(Iec61850State iecs)
        {
            return 0;
        }

        byte[] def_calledPresentationSelector = { 0x00, 0x00, 0x00, 0x01 };

        byte[] asn_id_as_acse = { 0x52, 0x01, 0x00, 0x01 };

        byte[] asn_id_mms = { 0x28, 0xca, 0x22, 0x02, 0x01 };

        byte[] ber_id = { 0x51, 0x01 };

        int encodeAcceptBer(byte[] buffer, int bufPos)
        {
            bufPos = OsiUtil.BerEncoder_encodeTL(0x30, 7, buffer, bufPos);
            bufPos = OsiUtil.BerEncoder_encodeTL(0x80, 1, buffer, bufPos);
            buffer[bufPos++] = 0;
            bufPos = OsiUtil.BerEncoder_encodeTL(0x81, 2, buffer, bufPos);
            buffer[bufPos++] = 0x51;
            buffer[bufPos++] = 0x01;

            return bufPos;
        }

        int encodeUserData(byte[] buffer, int bufPos, byte[] payload, int payloadLength, bool encode, byte contextId)
        {
            int encodedDataSetLength = 3; /* presentation-selector */

            /* presentation-data */
            encodedDataSetLength += payloadLength + 1;
            encodedDataSetLength += OsiUtil.BerEncoder_determineLengthSize((uint)payloadLength);

            int fullyEncodedDataLength = encodedDataSetLength;

            fullyEncodedDataLength += OsiUtil.BerEncoder_determineLengthSize((uint)encodedDataSetLength) + 1;

            if (encode)
            {
                /* fully-encoded-data */
                bufPos = OsiUtil.BerEncoder_encodeTL(0x61, (uint)fullyEncodedDataLength, buffer, bufPos);
                bufPos = OsiUtil.BerEncoder_encodeTL(0x30, (uint)encodedDataSetLength, buffer, bufPos);

                /* presentation-selector acse */
                bufPos = OsiUtil.BerEncoder_encodeTL(0x02, 1, buffer, bufPos);
                buffer[bufPos++] = contextId;

                /* presentation-data (= acse payload) */
                bufPos = OsiUtil.BerEncoder_encodeTL(0xa0, (uint)payloadLength, buffer, bufPos);

                return bufPos;
            }
            else
            {
                int encodedUserDataLength = fullyEncodedDataLength + 1;
                encodedUserDataLength += OsiUtil.BerEncoder_determineLengthSize((uint)fullyEncodedDataLength);

                return encodedUserDataLength;
            }
        }

        int createConnectPdu(byte[] buffer, byte[] payload, int payloadLength)
        {
            int contentLength = 0;

            /* mode-selector */
            contentLength += 5;

            int normalModeLength = 0;

            /* called- and calling-presentation-selector */
            normalModeLength += 12;

            int pclLength = 35;

            normalModeLength += pclLength;

            normalModeLength += encodeUserData(null, 0, payload, payloadLength, false, acseContextId);

            normalModeLength += 2;

            contentLength += normalModeLength; // + 2;

            contentLength += 1 + OsiUtil.BerEncoder_determineLengthSize((uint)normalModeLength);

            int bufPos = 0;

            bufPos = OsiUtil.BerEncoder_encodeTL(0x31, (uint)contentLength, buffer, bufPos);

            /* mode-selector */
            bufPos = OsiUtil.BerEncoder_encodeTL(0xa0, 3, buffer, bufPos);
            bufPos = OsiUtil.BerEncoder_encodeTL(0x80, 1, buffer, bufPos);
            buffer[bufPos++] = 1; /* 1 = normal-mode */

            /* normal-mode-parameters */
            bufPos = OsiUtil.BerEncoder_encodeTL(0xa2, (uint)normalModeLength, buffer, bufPos);

            /* calling-presentation-selector */
            bufPos = OsiUtil.BerEncoder_encodeTL(0x81, 4, buffer, bufPos);
            buffer[bufPos++] = (byte)((callingPresentationSelector >> 24) & 0xff);
            buffer[bufPos++] = (byte)((callingPresentationSelector >> 16) & 0xff);
            buffer[bufPos++] = (byte)((callingPresentationSelector >> 8) & 0xff);
            buffer[bufPos++] = (byte)(callingPresentationSelector & 0xff);

            /* called-presentation-selector */
            bufPos = OsiUtil.BerEncoder_encodeTL(0x82, 4, buffer, bufPos);
            buffer[bufPos++] = (byte)((calledPresentationSelector >> 24) & 0xff);
            buffer[bufPos++] = (byte)((calledPresentationSelector >> 16) & 0xff);
            buffer[bufPos++] = (byte)((calledPresentationSelector >> 8) & 0xff);
            buffer[bufPos++] = (byte)(calledPresentationSelector & 0xff);

            /* presentation-context-id list */
            bufPos = OsiUtil.BerEncoder_encodeTL(0xa4, 35, buffer, bufPos);

            /* acse context list item */
            bufPos = OsiUtil.BerEncoder_encodeTL(0x30, 15, buffer, bufPos);

            bufPos = OsiUtil.BerEncoder_encodeTL(0x02, 1, buffer, bufPos);
            buffer[bufPos++] = 1;

            bufPos = OsiUtil.BerEncoder_encodeTL(0x06, 4, buffer, bufPos);
            //memcpy(buffer + bufPos, asn_id_as_acse, 4);
            asn_id_as_acse.CopyTo(buffer, bufPos);
            bufPos += 4;

            bufPos = OsiUtil.BerEncoder_encodeTL(0x30, 4, buffer, bufPos);
            bufPos = OsiUtil.BerEncoder_encodeTL(0x06, 2, buffer, bufPos);
            //memcpy(buffer + bufPos, ber_id, 2);
            ber_id.CopyTo(buffer, bufPos);
            bufPos += 2;

            /* mms context list item */
            bufPos = OsiUtil.BerEncoder_encodeTL(0x30, 16, buffer, bufPos);

            bufPos = OsiUtil.BerEncoder_encodeTL(0x02, 1, buffer, bufPos);
            buffer[bufPos++] = 3;

            bufPos = OsiUtil.BerEncoder_encodeTL(0x06, 5, buffer, bufPos);
            //memcpy(buffer + bufPos, asn_id_mms, 5);
            asn_id_mms.CopyTo(buffer, bufPos);
            bufPos += 5;

            bufPos = OsiUtil.BerEncoder_encodeTL(0x30, 4, buffer, bufPos);
            bufPos = OsiUtil.BerEncoder_encodeTL(0x06, 2, buffer, bufPos);
            //memcpy(buffer + bufPos, ber_id, 2);
            ber_id.CopyTo(buffer, bufPos);
            bufPos += 2;

            /* encode user data */
            bufPos = encodeUserData(buffer, bufPos, payload, payloadLength, true, acseContextId);

            payload.CopyTo(buffer, bufPos);

            /*
            writeBuffer->partLength = bufPos;
            writeBuffer->length = bufPos + payload->length;
            writeBuffer->nextPart = payload;*/
            return bufPos + payloadLength;
        }

        int parseFullyEncodedData(byte[] buffer, int len, int bufPos)
        {
            int presentationSelector = -1;
            bool userDataPresent = false;

            int endPos = bufPos + len;

            if (buffer[bufPos++] != 0x30)
            {
                logger.LogDebug("PRES: user-data parse error");
                return -1;
            }

            bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, endPos);

            endPos = bufPos + len;

            if (bufPos < 0)
            {
                logger.LogDebug("PRES: wrong parameter length");
                return -1;
            }

            while (bufPos < endPos)
            {
                byte tag = buffer[bufPos++];
                int length = 0;

                bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref length, bufPos, endPos);

                if (bufPos < 0)
                {
                    logger.LogDebug("PRES: wrong parameter length");
                    return -1;
                }

                switch (tag)
                {
                    case 0x02: /* presentation-context-identifier */
                        logger.LogDebug("PRES: presentation-context-identifier");
                        {
                            presentationSelector = (int)OsiUtil.BerDecoder_decodeUint32(buffer, length, bufPos);
                            nextContextId = (byte)presentationSelector;
                            bufPos += length;
                        }
                        break;

                    case 0xa0:
                        logger.LogDebug("PRES: fully-encoded-data");

                        userDataPresent = true;

                        nextPayload_bufferIndex = bufPos;
                        nextPayload_size = length;

                        bufPos += length;
                        break;
                    default:
                        logger.LogDebug(String.Format("PRES: fed: unknown tag 0x{0:X2}", tag));

                        bufPos += length;
                        break;
                }
            }

            if (!userDataPresent)
            {
                logger.LogDebug("PRES: user-data not present\n");
                return -1;
            }

            return bufPos;
        }

        int parsePCDLEntry(byte[] buffer, int totalLength, int bufPos)
        {
            int endPos = bufPos + totalLength;

            int contextId = -1;
            bool isAcse = false;
            bool isMms = false;

            while (bufPos < endPos)
            {
                byte tag = buffer[bufPos++];
                int len = 0;

                bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, endPos);

                switch (tag)
                {
                    case 0x02: /* presentation-context-identifier */
                        contextId = (int)OsiUtil.BerDecoder_decodeUint32(buffer, len, bufPos);
                        bufPos += len;
                        break;
                    case 0x06: /* abstract-syntax-name */
                        logger.LogDebug(String.Format("PRES: abstract-syntax-name with len {0}", len));

                        if (len == 5)
                        {
                            isMms = true;
                            for (int i = 0; i < 5; i++)
                                if (buffer[bufPos + i] != asn_id_mms[i])
                                    isMms = false;
                            //if (memcmp(buffer + bufPos, asn_id_mms, 5) == 0)
                            //    isMms = true;
                        }
                        else if (len == 4)
                        {
                            isAcse = true;
                            for (int i = 0; i < 4; i++)
                                if (buffer[bufPos + i] != asn_id_as_acse[i])
                                    isAcse = false;
                            //if (memcmp(buffer + bufPos, asn_id_as_acse, 4) == 0)
                            //    isAcse = true;
                        }

                        bufPos += len;

                        break;
                    case 0x30: /* transfer-syntax-name */
                        logger.LogDebug("PRES: ignore transfer-syntax-name");

                        bufPos += len;
                        break;
                    default:
                        logger.LogDebug("PRES: unknown tag in presentation-context-definition-list-entry");
                        bufPos += len;
                        break;
                }
            }

            if (contextId < 0)
            {
                logger.LogDebug("PRES: ContextId not defined!");
                return -1;
            }

            if ((isAcse == false) && (isMms == false))
            {
                logger.LogDebug("PRES: not an ACSE nor MMS context definition");

                return -1;
            }

            if (isMms)
            {
                mmsContextId = (byte)contextId;
                logger.LogDebug(String.Format("PRES: MMS context id is {0}", contextId));
            }
            else
            {
                acseContextId = (byte)contextId;
                logger.LogDebug(String.Format("PRES: ACSE context id is {0}", contextId));
            }

            return bufPos;
        }

        int parsePresentationContextDefinitionList(byte[] buffer, int totalLength, int bufPos)
        {
            int endPos = bufPos + totalLength;

            while (bufPos < endPos)
            {
                byte tag = buffer[bufPos++];
                int len = 0;

                bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, endPos);

                switch (tag)
                {
                    case 0x30:
                        logger.LogDebug("PRES: parse pcd entry");
                        bufPos = parsePCDLEntry(buffer, len, bufPos);
                        if (bufPos < 0)
                            return -1;
                        break;
                    default:
                        logger.LogDebug("PRES: unknown tag in presentation-context-definition-list");
                        bufPos += len;
                        break;
                }
            }

            return bufPos;
        }

        int parseNormalModeParameters(byte[] buffer, int totalLength, int bufPos)
        {
            int endPos = bufPos + totalLength;

            while (bufPos < endPos)
            {
                byte tag = buffer[bufPos++];
                int len = 0;

                bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, endPos);

                if (bufPos < 0)
                {
                    logger.LogDebug("PRES: wrong parameter length");
                    return -1;
                }

                switch (tag)
                {
                    case 0x81: /* calling-presentation-selector */
                        logger.LogDebug("PRES: calling-pres-sel");
                        bufPos += len;
                        break;
                    case 0x82: /* calling-presentation-selector */
                        logger.LogDebug("PRES: calling-pres-sel");
                        bufPos += len;
                        break;
                    case 0xa4: /* presentation-context-definition list */
                        logger.LogDebug("PRES: pcd list");
                        bufPos = parsePresentationContextDefinitionList(buffer, len, bufPos);
                        break;
                    case 0x61: /* user data */
                        logger.LogDebug("PRES: user-data");

                        bufPos = parseFullyEncodedData(buffer, len, bufPos);

                        if (bufPos < 0)
                            return -1;

                        break;

                    default:
                        logger.LogDebug("PRES: unknown tag in normal-mode");
                        bufPos += len;
                        break;
                }
            }

            return bufPos;
        }

        public int parseAcceptMessage(byte[] buffer, int length)
        {
            int maxBufPos = length;

            int bufPos = 0;

            byte cpTag = buffer[bufPos++];

            if (cpTag != 0x31)
            {
                logger.LogDebug("PRES: not a CPA message\n");
                return 0;
            }

            int len = 0;

            bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, maxBufPos);

            while (bufPos < maxBufPos)
            {
                byte tag = buffer[bufPos++];

                bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, maxBufPos);

                if (bufPos < 0)
                {
                    logger.LogDebug("PRES: wrong parameter length\n");
                    return 0;
                }

                switch (tag)
                {
                    case 0xa0: /* mode-selector */
                        bufPos += len; /* ignore content since only normal mode is allowed */
                        break;
                    case 0xa2: /* normal-mode-parameters */
                        bufPos = parseNormalModeParameters(buffer, len, bufPos);

                        if (bufPos < 0)
                        {
                            logger.LogDebug("PRES: error parsing normal-mode-parameters");
                            return 0;
                        }

                        break;
                    default:
                        logger.LogDebug(String.Format("PRES: CPA unknown tag {0}", tag));
                        bufPos += len;
                        break;
                }
            }

            return 1;
        }

        public void init()
        {

        }

        public int createUserData(byte[] buffer, byte[] payload, int payloadLength)
        {
            int bufPos = 0;

            int userDataLengthFieldSize = OsiUtil.BerEncoder_determineLengthSize((uint)payloadLength);

            int pdvListLength = payloadLength + (userDataLengthFieldSize + 4);

            int pdvListLengthFieldSize = OsiUtil.BerEncoder_determineLengthSize((uint)pdvListLength);
            int presentationLength = pdvListLength + (pdvListLengthFieldSize + 1);

            bufPos = OsiUtil.BerEncoder_encodeTL(0x61, (uint)presentationLength, buffer, bufPos);

            bufPos = OsiUtil.BerEncoder_encodeTL(0x30, (uint)pdvListLength, buffer, bufPos);

            buffer[bufPos++] = (byte)0x02;
            buffer[bufPos++] = (byte)0x01;
            buffer[bufPos++] = (byte)mmsContextId; /* mms context id */

            bufPos = OsiUtil.BerEncoder_encodeTL(0xa0, (uint)payloadLength, buffer, bufPos);

            /*writeBuffer->partLength = bufPos;
            writeBuffer->length = bufPos + payloadLength;
            writeBuffer->nextPart = payload;*/

            payload.CopyTo(buffer, bufPos);
            return bufPos + payloadLength;
        }

        public int createUserDataACSE(byte[] buffer, byte[] payload, int payloadLength)
        {
            int bufPos = 0;

            int userDataLengthFieldSize = OsiUtil.BerEncoder_determineLengthSize((uint)payloadLength);
            ;
            int pdvListLength = payloadLength + (userDataLengthFieldSize + 4);

            int pdvListLengthFieldSize = OsiUtil.BerEncoder_determineLengthSize((uint)pdvListLength);
            int presentationLength = pdvListLength + (pdvListLengthFieldSize + 1);

            bufPos = OsiUtil.BerEncoder_encodeTL(0x61, (uint)presentationLength, buffer, bufPos);

            bufPos = OsiUtil.BerEncoder_encodeTL(0x30, (uint)pdvListLength, buffer, bufPos);

            buffer[bufPos++] = (byte)0x02;
            buffer[bufPos++] = (byte)0x01;
            buffer[bufPos++] = (byte)acseContextId; /* ACSE context id */

            bufPos = OsiUtil.BerEncoder_encodeTL(0xa0, (uint)payloadLength, buffer, bufPos);

            /*writeBuffer->partLength = bufPos;
            writeBuffer->length = bufPos + payloadLength;
            writeBuffer->nextPart = payload;*/

            payload.CopyTo(buffer, bufPos);
            return bufPos + payloadLength;
        }

        public int parseUserData(byte[] buffer, int length)
        {
            int bufPos = 0;

            if (length < 9)
                return 0;

            if (buffer[bufPos++] != 0x61)
                return 0;

            int len = 0;

            bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, length);

            if (buffer[bufPos++] != 0x30)
                return 0;

            bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, length);

            if (buffer[bufPos++] != 0x02)
                return 0;

            if (buffer[bufPos++] != 0x01)
                return 0;

            nextContextId = buffer[bufPos++];

            if (buffer[bufPos++] != 0xa0)
                return 0;

            int userDataLength = 0;

            bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref userDataLength, bufPos, length);

            //ByteBuffer_wrap(&(nextPayload), buffer + bufPos, userDataLength, userDataLength);
            // ??????
            // data
            return bufPos;
            //return 1;
        }

        public int parseConnect(byte[] buffer, int length)
        {
            int maxBufPos = length;

            int bufPos = 0;

            byte cpTag = buffer[bufPos++];

            if (cpTag != 0x31)
            {
                logger.LogDebug("PRES: not a CP type");
                return 0;
            }

            int len = 0;

            bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, maxBufPos);

            logger.LogDebug(String.Format("PRES: CPType with len {0}", len));

            while (bufPos < maxBufPos)
            {
                byte tag = buffer[bufPos++];

                bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, maxBufPos);

                if (bufPos < 0)
                {
                    logger.LogDebug("PRES: wrong parameter length\n");
                    return 0;
                }

                switch (tag)
                {
                    case 0xa0: /* mode-selection */
                        {
                            if (buffer[bufPos++] != 0x80)
                            {
                                logger.LogDebug("PRES: mode-value of wrong type!");
                                return 0;
                            }
                            bufPos = OsiUtil.BerDecoder_decodeLength(buffer, ref len, bufPos, maxBufPos);
                            uint modeSelector = OsiUtil.BerDecoder_decodeUint32(buffer, len, bufPos);
                            logger.LogDebug(String.Format("PRES: modesel {0}", modeSelector));
                            bufPos += len;
                        }
                        break;
                    case 0xa2: /* normal-mode-parameters */
                        bufPos = parseNormalModeParameters(buffer, len, bufPos);

                        if (bufPos < 0)
                        {
                            logger.LogDebug("PRES: error parsing normal-mode-parameters");
                            return 0;
                        }

                        break;
                    default: /* unsupported element */
                        logger.LogDebug(String.Format("PRES: tag 0x{0:X2} not recognized\n", tag));
                        bufPos += len;
                        break;
                }
            }

            return 1;
        }

        public int createConnectPdu(OsiConnectionParameters parameters, byte[] buffer, byte[] payload, int payloadLength)
        {
            acseContextId = 1;
            mmsContextId = 3;
            callingPresentationSelector = parameters.localPSelector;
            calledPresentationSelector = parameters.remotePSelector;
            return createConnectPdu(buffer, payload, payloadLength);
        }

        public int createAbortUserMessage(byte[] buffer, byte[] payload, int payloadLength)
        {
            int contentLength = 0;

            contentLength = +encodeUserData(null, 0, payload, payloadLength, false, acseContextId);

            contentLength += OsiUtil.BerEncoder_determineLengthSize((uint)contentLength) + 1;

            int bufPos = 0;

            bufPos = OsiUtil.BerEncoder_encodeTL(0xa0, (uint)contentLength, buffer, bufPos);

            /* encode user data */
            bufPos = encodeUserData(buffer, bufPos, payload, payloadLength, true, acseContextId);

            /*writeBuffer->partLength = bufPos;
            writeBuffer->length = bufPos + payload->length;
            writeBuffer->nextPart = payload;*/

            payload.CopyTo(buffer, bufPos);
            return bufPos + payloadLength;
        }

        public int createCpaMessage(byte[] buffer, byte[] payload, int payloadLength)
        {
            int contentLength = 0;

            /* mode-selector */
            contentLength += 5;

            int normalModeLength = 0;

            normalModeLength += 6; /* responding-presentation-selector */

            normalModeLength += 20; /* context-definition-result-list */

            normalModeLength += encodeUserData(null, 0, payload, payloadLength, false, acseContextId);

            contentLength += normalModeLength;

            contentLength += OsiUtil.BerEncoder_determineLengthSize((uint)normalModeLength) + 1;

            int bufPos = 0;

            bufPos = OsiUtil.BerEncoder_encodeTL(0x31, (uint)contentLength, buffer, bufPos);

            /* mode-selector */
            bufPos = OsiUtil.BerEncoder_encodeTL(0xa0, 3, buffer, bufPos);
            bufPos = OsiUtil.BerEncoder_encodeTL(0x80, 1, buffer, bufPos);
            buffer[bufPos++] = 1; /* 1 = normal-mode */

            /* normal-mode-parameters */
            bufPos = OsiUtil.BerEncoder_encodeTL(0xa2, (uint)normalModeLength, buffer, bufPos);

            /* responding-presentation-selector */
            bufPos = OsiUtil.BerEncoder_encodeTL(0x83, 4, buffer, bufPos);
            //memcpy(buffer + bufPos, calledPresentationSelector, 4);
            def_calledPresentationSelector.CopyTo(buffer, bufPos);
            bufPos += 4;

            /* context-definition-result-list */
            bufPos = OsiUtil.BerEncoder_encodeTL(0xa5, 18, buffer, bufPos);
            bufPos = encodeAcceptBer(buffer, bufPos); /* accept for acse */
            bufPos = encodeAcceptBer(buffer, bufPos); /* accept for mms */

            /* encode user data */
            bufPos = encodeUserData(buffer, bufPos, payload, payloadLength, true, acseContextId);

            /*writeBuffer->partLength = bufPos;
            writeBuffer->length = bufPos + payload->length;
            writeBuffer->nextPart = payload;*/

            payload.CopyTo(buffer, bufPos);
            return bufPos + payloadLength;
        }
    }
}
