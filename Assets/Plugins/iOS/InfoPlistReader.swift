import Foundation

@_cdecl("GetAPIHost")
public func GetAPIHost() -> UnsafePointer<CChar>? {
    if let apiHost = Bundle.main.object(forInfoDictionaryKey: "API_HOST") as? String {
        return UnsafePointer<CChar>(strdup(apiHost))
    }
    return nil
}