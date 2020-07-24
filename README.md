# PDNPresets

## Cài đặt plugin thủ công

- Sao chép file `PDNPresets.dll` trong thư mục `PDNPresets/bin/Release/` vào thư mục `Effects` của Paint.NET (mặc định: `C:/Program Files/paint.net/Effects/`).

## Chỉnh sửa và build lại project

- Cài đặt Paint.NET Plugin Template cho Visual Studio:
```bash
https://marketplace.visualstudio.com/items?itemName=toehead2001.PdnVsTemples
```
- Mở project bằng Visual Studio với quyền administrator.
- Bấm F5 để debug hoặc F7 để build file .dll

*Nếu xảy ra lỗi không tìm thấy thư viện PaintDotNet hoặc F5 không khởi chạy được Paint.NET, vui lòng vào project properties, chỉnh lại đường dẫn Paint.NET trong tab **Build Events**, **Debug** và **Reference Paths***

## Tham khảo

[1] https://forums.getpaint.net/topic/116676-how-to-writeread-effectconfigtoken-list-tofrom-files/

[2] https://github.com/bsneeze/pdn-scriptlab

[3] https://github.com/rivy/OpenPDN