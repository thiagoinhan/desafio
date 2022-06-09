export interface IMakeDepositRequest {
    event: string,
    target: IMakeDepositTarget,
    origin: IMakeDepositOrigin,
    amount: number,
}

export interface IMakeDepositTarget {
    bank: string,
    branch: string,
    account: string,
}

export interface IMakeDepositOrigin {
    bank: string,
    branch: string,
    cpf: string,
}