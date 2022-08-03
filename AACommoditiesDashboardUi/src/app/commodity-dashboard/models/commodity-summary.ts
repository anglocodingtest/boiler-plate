export interface CommoditySummary {
	modelId: number;
	commodityId: number;
	modelName: string;
	commodityName: string;
	date: Date | string;
	position: number;
	pnlCurrent: number;
	price: number;
	pnlLtd: number;
	yearSummaries: YearSummary[];
}

export interface YearSummary {
	year: number;
	pnlYtd: number;
	drawdownYtd: number;
}
 