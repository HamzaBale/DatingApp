export interface Message{
    id: number,
    senderId: number,
    "senderUsername": string,
    "senderPhotoUrl": string,
    "recipientId": number,
    "recipientUsername": string,
    "recipientPhotoUrl": string,
    "content":string,
    "dateRead": Date,
    "messageSent": Date
}
export interface MessageParams{
    page?:number,
    pageSize?:number,
    predicate:("Inbox"|"Outbox"|"Unread")
}