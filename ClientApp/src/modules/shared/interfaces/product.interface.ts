export interface Product
{
  productId: number,
  clothName: string,
  clothType: boolean,
  remark: string,
  price: number  
  upper:
  {
    height: number,
    front: number,
    collar: number,
    shoulder: number,
    sleeve: number,
    sleeveWidth: number,
    cuff: number
  },
  lower:
  {
    height: number,
    knee: number,
    waist: number,
    seat: number,
    thigh: number,
    bottom: number,
    underline: number
  }
}
