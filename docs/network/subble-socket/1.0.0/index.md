# SubbleSocket 
> Version 1.0.0

## Overview

The SubbleSocket protocol enables a two-way communication between a ISubbleHost instance and a third party client.
The security model is out of scope of this document, being that the implementor responsibility.
This protocol aims at allowing external applications to consume and produce events in a Subble instance.

The protocol can be divided in two parts:
    - Handshake: the clients connects and send list of required events
    - Event Consume/Produce: the client listens for events and / or produce events

**Encoding**

Every text used in this format must be in UTF-8

**Case sensitive**

Every text is case sensitive except when explicity said in this document.

## Serialization

the server must support [MessagePack](https://msgpack.org/) and should also support [JSON](http://json.org/). The implementor is free to support custom serializers, but must not use the values between 0 (zero) and 50 (fifty) because they are reserved for future requirements. the current serializers map is as follow:

- 0: MessagePack
- 1: JSON
- 2 - 50: Reserved
- 51-255: Implementor defined

for the purpose of readability this document will the JSON format in examples.

## Handshake

When a Clients connects it must send the handshake initial request, the server will not continue with any comunication until the initial handshake is completed. A appropriate response is trigger by the server

## Handshake Request

The initial client request is composed by:

    1 byte : Protocol Major Version
    1 byte : Protocol Minor Version
    1 byte : Protocol Patch Version
    1 byte : Serialization format
    4 byte : Request data length
    N byte : Request data

The first three bytes are the version used by the client, if the server won't accept the version an error response is trigger,
see [Handshake Response](#handshake-response) for more details.

The fourth byte is the serialization format to use, see [Serialization section](#serialization) for more details.

The next four bytes are referent to the data length. And the remaining bytes are the request data.
The request data must contains the following fields:

1. **ServerSecret**: Secret shared by server
2. **ClientGUID**: Client identification
3. **Subscribe**: array of events to subscribe
4. **Produce**: array of events to produce

Now we will see each field in detail.

**1) ServerSecret**

This field must be present, but can have an empty value.
This is a secret shared by server to the client, if the secret does not match the servers, a error response may be trigger.
How this secret is generated and shared with the client its out of scope of this spec, but it must be encoded in UTF-8

**2) ClientGUID**

This field must be present, but can have an empty value.
This is a Global unique identifier that will be used by the server to identify the client, if this field is empty the server must generate one. The value must be UTF-8, but his generation is entirely up to the client or server

**3) Subscribe**

This field must be present, but can be an empty array.
this is an array containing the list of events to subscribe. Is up to server to respect this list, but it must never send events not in this list, so the client is not guaranteed that will receive all events, but is guaranteed not to receive extra events
If the client wants to subscribe to all events, the first element should be named `SUBSCRIBE_ALL`.

**4) Produce**

This field must be present, but can be an empty array.
This is an array containing the list of events that the client might produce, if the client trys to send an event not in this list an error response must be raised by the server.

##### Request example

```json
{
    "ServerSecret": "+YSe2ARFYupQyjS9lTLV6JNDP7yvk1njbyLgg03jKPDZl3kwuQrL7MxfTpQCIuxUZSowYmux4ZfKAsgBcFL/6g==",
    "ClientGUID": "cfa56597-4f78-4e16-82bc-8c0ae59548c2",
    "Subscribe": [
        "EVENT_CORE_LOG"
    ],
    "Produce": [
        "MY_CUSTOM_EVENT"
    ]
}
```

## Handshake Response
After the client sends the Handshake request the server responds. The response is composed by:

    4 byte : Response length
    N byte : Response

The response has the following fields:

1. ClientGUID: Client identification
2. Accept: defines the handshake result
3. ErrorCode: code of error

**1) ClientGUID**

If does not defined a GUID when creating the request, this will contain the server generated id.
The client should store this value and use it in future requests to allow for data persistance.

**2) Accept**

This field defines the handshake result. if false the handshake was rejected, the client should see ErrorCode for details

**3) ErrorCode**

If an error occur this field will have an number, see [Handshake Error Codes](#handshake-error-codes) for details.

#### Response example

```json
{
    "ClientGUID": "cfa56597-4f78-4e16-82bc-8c0ae59548c2",
    "Accept": false,
    "ErrorCode": 1
}
```

## Handshake Error Codes

- 0: Unknown Error

Server refused the connection for unknown reasons, check log for details

- 1: IP not allowed

Server is actively denying the client IP, check a possible whitelist or blacklist

- 2: New Clients not allowed

The client is new and the server actively refuses to register new clients

- 3: Invalid server secret

The secret sent by the client does not match the server one
