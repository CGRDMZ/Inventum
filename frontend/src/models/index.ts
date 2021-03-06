export interface BoardDto {
    boardId: string;
    name: string;
    bgColor: string;
}

export interface CreateBoardDto {
    name: string,
    color: string
}

export interface BoardDetailsDto {
    boardInfo: BoardInfoDto;
    cardGroups: CardGroupDto[];
    activities: ActivityDto[]
}

export interface ActivityDto {
    occuredOn: string;
    doneByUser: string;
    message: string;
}

export interface CardGroupDto {
    cardGroupId: string;
    name: string;
    cards: CardDto[]
}

export interface CardDto {
    cardId: string;
    content: string;
    cardColor: string;
    position: number;
}

export interface BoardInfoDto {
    boardId: string;
    name: string;
    bgColor: string;
}

export interface LoginDto {
    username: string;
    password: string;
}

export interface InvitationDto {
    invitationId: string;
    invitedTo: string;
}

export interface CreateCardGroupDto {
    cardGroupName: string;
}

export interface CreateCardDto {
    content: string;
    bgColor: string;
}

export interface InviteUserDto {
    boardId: string;
    invitedUserUsername: string;
}

export interface Result<T> {
    success: string;
    data: T;
    errors: string[];
}

export interface RegisterDto {
    username: string;
    email: string;
    password: string;
}

export interface CheckListItemDto {
    checkListItemId: string;
    content: string;
    isChecked: boolean;
    position: number;
}

export interface CheckListComponentDto {
    checkListComponentId: string;
    name: string;
    checkListItems: CheckListItemDto[];
}

export interface CardWithComponentsDto {
    cardId: string;
    content: string;
    description: string;
    cardColor: string;
    position: number;
    checkListComponents: CheckListComponentDto[];
}