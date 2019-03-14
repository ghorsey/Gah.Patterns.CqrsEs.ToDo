# A Sample CQRS/ES application.

A very simple list management app.

**ToDoList (Query Model)**

| Property       | Type     |
| -------------- | -------- |
| Id             | Guid     |
| Title          | Guid     |
| TotalItems     | int      |
| TotalCompleted | int      |
| TotalPending   | int      |
| Created        | DateTime |
| Updated        | DateTime |

**ToDoItem (Query Model)**

| Property | Type     |
| -------- | -------- |
| Id       | Guid     |
| ListId   | Guid     | 
| Title    | string   |
| IsDone   | bool     |
| Created  | DateTime |
| Updated  | DateTime |

**Events (Command Model)**

| Property  | Type     |
| --------  | -------- |
| Id        | Guid     |
| StreamId  | Guid     |
| Data      | object   |
| Version   | long     |
| EventName | string   |
| Created   | DateTime |

## Other thoughts and musings

1. To support rolling snapshots, similar to EventStore, I should create a metadata table that
   contains at minimum a TruncateBefore column to house the current starting point of the event
   stream.  This will allow a dev to create a "SnapshotCreated" type event, which will be assigned
   some event number.  Once that EventNumber is known, should be able to call a method like 
   `SetTruncateBefore("{stream id}", {Snapshot event number})`  Then loading the stream will continue
   from where `EventNumber` >= `{Snapshot event number}`