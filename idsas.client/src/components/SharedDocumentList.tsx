import "./SharedDocumentList.css";
import {useState} from "react";

export interface SharedDocument {
    name: string;
    linkToken: string;
    available: boolean;
}

interface SharedDocumentsListProps {
    title: string;
    documents: SharedDocument[];
    onDownloadFile: (index: number) => void;
    onFileAccess: (token: string) => void;
}

const MyDocumentsList: React.FC<SharedDocumentsListProps> = ({ title, documents, onDownloadFile, onFileAccess }) => {
    const [sharedDocumentToken, setSharedDocumentToken] = useState<string>("");
    const handleTokenChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSharedDocumentToken(event.target.value);
    };
    return (
        <div className="shared-documents-list-container">
            <h2>{title}</h2>
            <input
                type="text"
                placeholder="Enter shared document token"
                value={sharedDocumentToken}
                onChange={handleTokenChange}
            />
            <button className={"access-button"} onClick={() => {
                onFileAccess(sharedDocumentToken);
            }}>Access</button>
            <ul className="documents-list">
                {documents.map((document, index) => (
                    <li key={index} className="document-item">
                        <span className="document-name">{document.name}</span>
                        <span className={"document-status"}>
                            {document.available ? "Available" : "Awaiting Confirmation"}
                        </span>
                        {document.available && (
                            <img className={"download-button"} src={"./download.svg"} onClick={() => {
                                onDownloadFile(index);
                            }}></img>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MyDocumentsList;
