export class Meeting {
    constructor(
        public Id: number,     
        public Name: string,
        public Date: Date,
        public EmployeeId: number,
        public Duration: string,
    ) { }
}
