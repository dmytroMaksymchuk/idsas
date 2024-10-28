import "./MyDocumentList.css";
import {useEffect, useState} from "react";

export enum ShareState{
    shared = "Shared",
    private = "Private",
    waitingConfirmation = "Waiting for Confirmation",
}
export interface MyDocument {
    name: string;
    token: string;
    shareState: ShareState;
}

interface MyDocumentsListProps {
    title: string;
    documents: MyDocument[];
    onDownloadFile: (index: number) => void;
    confirmDocument: (index: number) => void;
    rejectDocument: (index: number) => void;
    onShareDocument: (index: number) => void;
}

const MyDocumentsList: React.FC<MyDocumentsListProps> = ({ title, documents, onDownloadFile, confirmDocument, rejectDocument, onShareDocument }) => {
    const [inspectDocumentID, setInspectDocumentID] = useState<number>(-1);
    const [manageConfirmation, setManageConfirmation] = useState<boolean>(false);

    useEffect(() => {
        documents.map((document, index) => {
            console.log(`Document ${index}: ${document.name}`);
            console.log(`Share State: ${document.shareState}`);
        });
    }, []);
    return (
        <div className="documents-list-container">
            <h2>{title}</h2>
            {inspectDocumentID == -1 && (
                <ul className="documents-list">
                    {documents.map((document, index) => (
                        <li key={index} className="document-item">
                            <span className="document-name">{document.name}</span>
                            <img className={"download-button"} src={"./download.svg"} onClick={() => {
                                onDownloadFile(index);
                            }}></img>
                            <span className="document-status">
                                {document.shareState}
                            </span>
                            <span className={"manage-access-button"} onClick={() => setInspectDocumentID(index)}>Manage Access</span>
                        </li>
                    ))}
                </ul>
            )}
            {inspectDocumentID != -1 && (
                <div className="document-inspection">
                    <h3>{documents[inspectDocumentID].name}</h3>
                    <div className={"share-state"}>
                        <span className={"share-state-text"}>Share State: </span>
                        {documents[inspectDocumentID].shareState == ShareState.shared && (
                            <>
                                <span className={"share-state-text-2"}>{ShareState.shared + " with ......"}</span>
                                <button className="manage-access-button">Revoke Access</button>
                            </>
                        )}
                        {documents[inspectDocumentID].shareState == ShareState.private && (
                            <>
                                <span className={"share-state-text-2"}>{ShareState.private}</span>
                                <button className="manage-access-button" onClick={() => {
                                    onShareDocument(inspectDocumentID)
                                }}> Share</button>
                                <select className={"share-dropdown"}>
                                    <option value="first">First to Access</option>
                                    <option value="public">Public</option>
                                </select>
                            </>
                        )}
                        {documents[inspectDocumentID].shareState == ShareState.waitingConfirmation && (
                            <>
                                <span className={"share-state-text-2"}>{ShareState.waitingConfirmation}</span>
                                <button className="manage-access-button" onClick={
                                    () => setManageConfirmation(!manageConfirmation)}>
                                    Manage
                                </button>

                                {manageConfirmation && (
                                    <div className={"confirmation-management"}>
                                        <h4 className={"requested-user"}>Access requested by user: B</h4>
                                        <button className="confirm-button" onClick={() => {
                                            setManageConfirmation(false);
                                            confirmDocument(inspectDocumentID);
                                        }}>Confirm</button>
                                        <button className="reject-button" onClick={() => {
                                            setManageConfirmation(false);
                                            rejectDocument(inspectDocumentID);
                                        }}>Reject</button>
                                    </div>
                                )}
                            </>
                        )}
                    </div>
                    <img className="close-inspection-button" src={"./close.svg"} onClick={() => setInspectDocumentID(-1)}></img>

                </div>
            )}
        </div>
    );
};

export default MyDocumentsList;
