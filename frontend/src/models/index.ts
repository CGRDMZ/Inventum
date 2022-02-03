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
    occuredOn: Date;

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

export interface loginDto {
    username: string;
    password: string;
}