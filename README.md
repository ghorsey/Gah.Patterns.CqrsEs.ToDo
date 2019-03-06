# A Sample CQRS/ES application.

A very simple list management app.

**List (Query Model)**

| Property       | Type     |
| -------------- | -------- |
| Id             | Guid     |
| Title          | Guid     |
| TotalItems     | int      |
| TotalCompleted | int      |
| TotalPending   | int      |
| Created        | DateTime |
| Updated        | DateTime |

**Item (Query Model)**

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
