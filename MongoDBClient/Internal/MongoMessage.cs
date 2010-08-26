﻿/* Copyright 2010 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MongoDB.MongoDBClient.Internal {
    public abstract class MongoMessage {
        #region protected fields
        protected int messageLength;
        protected int requestId;
        protected int responseTo;
        protected MessageOpcode opcode;
        #endregion

        #region constructors
        protected MongoMessage(
            MessageOpcode opcode
        ) {
            this.opcode = opcode;
        }
        #endregion

        #region public properties
        public int MessageLength {
            get { return messageLength; }
        }

        public int RequestId {
            get { return requestId; }
        }

        public int ResponseTo {
            get { return responseTo; }
        }

        public MessageOpcode Opcode {
            get { return opcode; }
        }
        #endregion

        #region protected methods
        protected void ReadMessageHeaderFrom(
            BinaryReader reader
        ) {
            messageLength = reader.ReadInt32();
            requestId = reader.ReadInt32();
            responseTo = reader.ReadInt32();
            if ((MessageOpcode) reader.ReadInt32() != opcode) {
                throw new MongoException("Message header opcode was not the expected one");
            }
        }

        protected void WriteMessageHeaderTo(
            BinaryWriter writer
        ) {
            writer.Write(0); // messageLength will be backpatched later
            writer.Write(requestId);
            writer.Write(0); // responseTo not used in requests sent by client
            writer.Write((int) opcode);
        }
        #endregion
    }
}