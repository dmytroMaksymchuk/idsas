import "./SharedDocumentList.css";

export interface SharedDocument {
    name: string;
}

interface SharedDocumentsListProps {
    title: string;
    documents: SharedDocument[];
    onDownloadFile: (index: number) => void;
}

const MyDocumentsList: React.FC<SharedDocumentsListProps> = ({ title, documents, onDownloadFile }) => {
    return (
        <div className="shared-documents-list-container">
            <h2>{title}</h2>
            <ul className="documents-list">
                {documents.map((document, index) => (
                    <li key={index} className="document-item">
                        <span className="document-name">{document.name}</span>
                        <img className={"download-button"} src={"./download.svg"} onClick={() => {
                            onDownloadFile(index);
                        }}></img>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MyDocumentsList;
